using Mirror;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Cell : NetworkBehaviour
{
    [SyncVar] public bool IsLocked = true;
    [SyncVar] public bool IsAvailable = true;

    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Color32 _hoverColor;

    private Color32 _oldColor;

    private MaterialPropertyBlock _newBlock;
    private MaterialPropertyBlock _oldBlock;

    private GameItem _item;

    private Vector2Int[] _coordinates = new Vector2Int[8]
    {
        new Vector2Int(0,1),
        new Vector2Int(1,1),
        new Vector2Int(1,0),
        new Vector2Int(1,-1),
        new Vector2Int(0,-1),
        new Vector2Int(-1,-1),
        new Vector2Int(-1,0),
        new Vector2Int(-1,1),
    };

    [SerializeField] private TempDictionary<Vector2Int, Cell> _neighborPairs;

    private Dictionary<Vector2Int, Cell> _neighbors = new Dictionary<Vector2Int, Cell>();

    public Dictionary<Vector2Int, Cell> Neighbors => _neighbors;
    public GameItem Item => _item;

    public event Action<Cell> Clicked;

    protected override void OnValidate()
    {
        base.OnValidate();
        SetNeighbors();
    }

    private void Awake()
    {
        _newBlock = new MaterialPropertyBlock();
        _oldBlock = new MaterialPropertyBlock();

        _oldColor = _meshRenderer.material.color;

        _newBlock.SetColor("_Color", _hoverColor);
        _oldBlock.SetColor("_Color", _oldColor);

        _neighbors = _neighborPairs.ToDictionary();
    }

    public void SetNeighbors()
    {
        _neighborPairs.Clear();

        foreach (var coordinate in _coordinates)
        {
            Vector3 origin = transform.position;
            Vector3 direction = new Vector3(coordinate.x, 0, coordinate.y);

            if (Physics.Raycast(origin, direction, out RaycastHit hit, 10f))
                if (hit.collider.TryGetComponent(out Cell cell))
                    _neighborPairs.Add(coordinate, cell);
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdLock()
    {
        IsLocked = true;
    }

    [Command(requiresAuthority = false)]
    public void CmdUnlock()
    {
        IsLocked = false;
    }

    public GameObject Create(GameItemType type, GameObject prefab)
    {
        _item = Instantiate(prefab).GetComponent<GameItem>();

        _item.SetType(type);
        _item.transform.position = transform.position;

        IsAvailable = false;

        return _item.gameObject;
    }

    public void Highlight(MaterialPropertyBlock block)
    {
        if (IsLocked)
            return;

        _meshRenderer.SetPropertyBlock(block);
    }

    private void OnMouseEnter()
    {
        if (IsAvailable)
            Highlight(_newBlock);
    }

    private void OnMouseExit()
    {
        if (IsAvailable)
            Highlight(_oldBlock);
    }

    private void OnMouseUpAsButton()
    {
        Highlight(_oldBlock);
        Clicked?.Invoke(this);
    }
}
