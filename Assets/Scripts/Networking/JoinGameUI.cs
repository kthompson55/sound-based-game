using UnityEngine;
using System.Collections;
using UnityEngine.Networking.Match;

public class JoinGameUI : MonoBehaviour {

    private bool matchsFound = false;


    public void MyOnClick()
    {
        MyNetworkManager.singleton.matchMaker.ListMatches(0, 20, "", OnMatchList);

        //GameObject.Find("NetworkManager").GetComponent<MyNetworkManager>().JoinGame();
    }

    public void JoinGame(int locatioin)
    {

    }

    public void OnMatchList(ListMatchResponse matchListResponse)
    {
        if (matchListResponse.success && matchListResponse.matches != null)
        {
            matchsFound = true;
            MyNetworkManager.singleton.OnMatchList(matchListResponse);
        }

    }

    void Update()
    {
        if (matchsFound)
        {
            Application.LoadLevel("MultiplayerLobby");
        }
    }
}
