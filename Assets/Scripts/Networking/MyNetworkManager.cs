using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class MyNetworkManager : NetworkManager {

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
<<<<<<< HEAD
        Vector3 spawnPos = new Vector3(60f, 66f, 135f);
=======
        Vector3 spawnPos = new Vector3(-20.0f, 1.5f, -14.0f);
>>>>>>> origin/Week2-Playtest
        GameObject player;
        if (conn.connectionId < 0)
        {
            player = (GameObject)Instantiate(base.playerPrefab, spawnPos, Quaternion.identity);
        }
        else
        {
            player = (GameObject)Instantiate(spawnPrefabs[0], spawnPos, Quaternion.identity);
        }

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
