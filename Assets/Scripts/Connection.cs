using Mirror;
using UnityEngine;

public class Connection : MonoBehaviour
{
    [SerializeField] private NetworkManager _networkManager;

    private void Start()
    {
        if (Application.isBatchMode)
            Debug.Log("Started with batch mode!");
        else
            Debug.Log("Started without batch mode!");
    }

    public void JoinClient()
    {
        _networkManager.networkAddress = "localhost";
        _networkManager.StartClient();
    }
}
