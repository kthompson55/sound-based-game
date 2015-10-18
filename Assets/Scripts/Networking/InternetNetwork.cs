using UnityEngine;
using System.Collections;
using UnityEngine.Networking.Match;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;

public class InternetNetwork : MonoBehaviour {

    List<MatchDesc> matchList = new List<MatchDesc>();
    bool matchCreated;
    NetworkMatch networkMatch;

    void Awake()
    {
        networkMatch = gameObject.AddComponent<NetworkMatch>();
        Debug.Log("Awake");
        NetworkManager.singleton.matchMaker = networkMatch;
        

    }

    void OnGUI()
    {
        if (GUILayout.Button("Start Match"))
        {
            CreateMatchRequest create = new CreateMatchRequest();
            create.name = "NewRoom";
            create.size = 4;
            create.advertise = true;
            create.password = "";

            networkMatch.CreateMatch(create, OnMatchCreate);
        }

        if (GUILayout.Button("List rooms"))
        {
            networkMatch.ListMatches(0, 20, "", OnMatchList);
        }

        if (matchList.Count > 0)
        {
            GUILayout.Label("Current rooms");
        }

        foreach (var m in matchList)
        {
            if (GUILayout.Button(m.name))
            {
                networkMatch.JoinMatch(m.networkId, "", OnMatchJoined);
            }
        }

    }

    public void OnMatchCreate(CreateMatchResponse matchResponse)
    {
        if (matchResponse.success)
        {
            Debug.Log("Create match succeeded");
            matchCreated = true;
            Utility.SetAccessTokenForNetwork(matchResponse.networkId, new NetworkAccessToken(matchResponse.accessTokenString));
            NetworkServer.Listen(new MatchInfo(matchResponse), 9000);
        }
        else
        {
            Debug.Log("Create match failed");
        }
    }

    public void OnMatchList(ListMatchResponse matchListResponse)
    {
        Debug.Log("OnMatchList Entered");
        if (matchListResponse.success && matchListResponse.matches != null)
        {
            Debug.Log("matchListResponse.success && matchListResponse.matches != null statement true");
            networkMatch.JoinMatch(matchListResponse.matches[0].networkId, "", OnMatchJoined);
        }
    }

    public void OnMatchJoined(JoinMatchResponse matchJoin)
    {
        if (matchJoin.success)
        {
            Debug.Log("Join match succeeded");
            if (matchCreated)
            {
                Debug.LogWarning("Match alread set up, abort...");
                return;
            }
            Utility.SetAccessTokenForNetwork(matchJoin.networkId, new NetworkAccessToken(matchJoin.accessTokenString));
            NetworkClient myClient = new NetworkClient();
            myClient.RegisterHandler(MsgType.Connect, OnConnected);
            myClient.Connect(new MatchInfo(matchJoin));
        }
    }

    public void OnConnected(NetworkMessage msg)
    {
        Debug.Log("Connected!");
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
