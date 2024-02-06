using UnityEngine;

[CreateAssetMenu(fileName = "GameItemData")]
public class GameItemData : ScriptableObject
{
    [SerializeField] private GameItem _prefab;
    [SerializeField] private GameItemType _type;
    [SerializeField] private Sprite _icon;

    public GameItemType Type => _type;
    public GameItem Prefab => _prefab;
    public Sprite Icon => _icon;
}
