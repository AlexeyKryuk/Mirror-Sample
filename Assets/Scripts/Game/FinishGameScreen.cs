using Mirror;
using UnityEngine;

public class FinishGameScreen : NetworkBehaviour
{
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _loseScreen;

    [ClientRpc]
    public void RpcOnGameFinished(GameItemType type)
    {
        if (Player.Instance.GameItemType == type)
            _winScreen.SetActive(true);
        else
            _loseScreen.SetActive(true);
    }
}
