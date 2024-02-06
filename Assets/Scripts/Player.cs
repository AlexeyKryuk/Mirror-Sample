using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SyncVar] public GameItemType GameItemType;

    [SerializeField] private Camera _camera;

    public static Player Instance;

    [TargetRpc]
    public void RpcInit(NetworkConnectionToClient target, GameItemType type, string name)
    {
        GameItemType = type;
        _camera.enabled = true;

        Instance = this;
    }
}
