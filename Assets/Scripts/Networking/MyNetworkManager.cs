using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class MyNetworkManager : NetworkManager 
{
    public Vector3 spawnPos;

    void Start()
    {
        //NetworkManager.singleton.StartMatchMaker();
        //singleton.matchSize = 4;
        //singleton.matchName = "name";
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        Vector3 spawnPos = new Vector3(5.54f, 1.77f, -5.49f);
        GameObject temp=GameObject.Find("PhysicalCamera");
        if(temp!=null){
            spawnPos = temp.GetComponent<PysicalCameraScript>().pos;
        }
        Debug.Log("Connection ID" + conn.connectionId);

        GameObject player;
        if (conn.connectionId < 0)
        {
            //player = (GameObject)Instantiate(base.playerPrefab, spawnPos, Quaternion.identity);
            player = (GameObject)Instantiate(spawnPrefabs[1], spawnPos, Quaternion.identity);
        }
        else
        {
            player = (GameObject)Instantiate(spawnPrefabs[0], spawnPos, Quaternion.identity);
        }

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

    public void SetUpNetworking()
    {
        Debug.Log("Seting up networking");
        NetworkManager.singleton.StartMatchMaker();
        singleton.matchSize = 4;
        singleton.matchName = "name";
    }

    public void StartGame(string textName)
    {
        SetUpNetworking();
        singleton.matchName = textName;

        MyNetworkManager.singleton.matchMaker.CreateMatch(matchName, matchSize, true, "", OnMatchCreate);
        //NetworkManager.singleton.StartHost();
    }

    public void JoinGame(int location)
    {
        if (singleton.matches == null)
        {
            singleton.matchMaker.ListMatches(0, 20, "", singleton.OnMatchList);
        }
        else
        {
            singleton.matchMaker.JoinMatch(matches[location].networkId, "", singleton.OnMatchJoined);
        }
      //  NetworkManager.singleton.StartClient();
    }

    public bool AreMatches()
    {
        return MyNetworkManager.singleton.matches != null;
    }
}
