using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemSpawner : NetworkBehaviour
{
    [SyncVar] public GameItemType CurrentPlayer = GameItemType.Sphere;

    [SerializeField] private Transform _content;
    [SerializeField] private ItemUI _itemUIPrefab;
    [SerializeField] private CellCollection _cellCollection;
    [SerializeField] private List<GameItemData> _itemDatas;

    private ItemUI _currentItem;
    private List<ItemUI> _UIitems = new List<ItemUI>();

    public List<Cell> Cells => _cellCollection.Cells;

    public event Action<Cell> Spawned;

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => Player.Instance != null);

        foreach (var data in _itemDatas)
        {
            if (data.Type == Player.Instance.GameItemType)
            {
                var itemUI = Instantiate(_itemUIPrefab, _content);
                itemUI.Init(data);

                _UIitems.Add(itemUI);
            }
        }

        foreach (var item in _UIitems)
            item.Clicked += OnItemClicked;

        foreach (var cell in Cells)
            cell.Clicked += OnCellClicked;
    }

    private void OnDisable()
    {
        foreach (var item in _UIitems)
            item.Clicked -= OnItemClicked;

        foreach (var cell in Cells)
            cell.Clicked -= OnCellClicked;
    }

    [Command(requiresAuthority = false)]
    public void CmdSpawn(Cell cell, GameItemType type)
    {
        if (CurrentPlayer != type)
            return;

        GameObject prefab = NetworkManager.singleton.spawnPrefabs.First(prefab =>
        prefab.name == type.ToString());

        var item = cell.Create(type, prefab);

        NetworkServer.Spawn(item);

        Spawned?.Invoke(cell);

        CurrentPlayer = CurrentPlayer == GameItemType.Sphere ? GameItemType.Box : GameItemType.Sphere;
    }

    private void OnItemClicked(ItemUI item)
    {
        if (_currentItem != item)
        {
            _currentItem = item;

            foreach (var cell in Cells)
                cell.CmdUnlock();
        }
        else
        {
            _currentItem = null;

            foreach (var cell in Cells)
                cell.CmdLock();
        }

        foreach (var other in _UIitems)
            if (other != _currentItem)
                other.Cancel();
    }

    private void OnCellClicked(Cell cell)
    {
        if (_currentItem == null || cell.IsLocked || !cell.IsAvailable)
            return;

        if (CurrentPlayer != Player.Instance.GameItemType)
            return;

        if (_currentItem.Data.Type == GameItemType.Sphere)
            CmdSpawn(cell, GameItemType.Sphere);
        else
            CmdSpawn(cell, GameItemType.Box);

        foreach (var item in _UIitems)
            item.Cancel();

        foreach (var other in Cells)
            other.CmdLock();

        _currentItem = null;
    }
}
