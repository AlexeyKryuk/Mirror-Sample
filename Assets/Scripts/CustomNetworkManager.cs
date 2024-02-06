using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomNetworkManager : NetworkManager
{
    private Stack<GameItemType> _gameItems = new Stack<GameItemType>();

    public override void OnStartServer()
    {
        base.OnStartServer();

        _gameItems.Push(GameItemType.Box);
        _gameItems.Push(GameItemType.Sphere);

        NetworkServer.RegisterHandler<CreateCharacterMessage>(OnCreateCharacter);
    }

    public override void OnClientConnect()
    {
        base.OnClientConnect();

        NetworkClient.Send(new CreateCharacterMessage());
    }

    private void OnCreateCharacter(NetworkConnectionToClient conn, CreateCharacterMessage message)
    {
        Transform spawnPosition = GetStartPosition();

        Debug.Log(spawnPosition.localPosition + " " + spawnPosition.localRotation);

        Player player = Instantiate(playerPrefab, spawnPosition.localPosition, spawnPosition.localRotation).GetComponent<Player>();
        NetworkServer.AddPlayerForConnection(conn, player.gameObject);

        player.RpcInit(conn, _gameItems.Pop(), "123");
    }
}