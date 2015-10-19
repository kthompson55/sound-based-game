﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class MyNetworkManager : NetworkManager {

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        Vector3 spawnPos = new Vector3(59.97f, 65f, 136f);

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
