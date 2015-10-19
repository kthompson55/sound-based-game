using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class MyNetworkManager : NetworkManager {

    void Start()
    {
        NetworkManager.singleton.StartMatchMaker();
        singleton.matchSize = 4;
        singleton.matchName = "name";
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        Vector3 spawnPos = new Vector3(60f, 66f, 135f);

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
        NetworkManager.singleton.matchMaker.CreateMatch(singleton.matchName, singleton.matchSize, true, "", singleton.OnMatchCreate);
        //NetworkManager.singleton.StartHost();
    }

    public void JoinGame()
    {
        if (singleton.matches == null)
        {
            singleton.matchMaker.ListMatches(0, 20, "", singleton.OnMatchList);
        }
        else
        {
            singleton.matchMaker.JoinMatch(matches[0].networkId, "", singleton.OnMatchJoined);
        }
      //  NetworkManager.singleton.StartClient();
    }
}
