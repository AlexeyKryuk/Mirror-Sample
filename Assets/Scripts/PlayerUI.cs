using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    
    private Player _player;

    public void SetPlayer(Player player)
    {
        _player = player;
        _name.text = _player.name;
    }
}
