using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class MyNetworkManager : NetworkManager {

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        Vector3 spawnPos = Vector3.back * conn.connectionId * 2;
        GameObject player = (GameObject)Instantiate(base.playerPrefab, spawnPos, Quaternion.identity);

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

    public void StartGame()
    {
        NetworkManager.singleton.StartHost();
    }

    public void JoinGame()
    {
        NetworkManager.singleton.StartClient();
    }
}
