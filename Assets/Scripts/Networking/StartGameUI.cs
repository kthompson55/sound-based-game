﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartGameUI : MonoBehaviour {

    //Called in main menu to switch scenes
    public void MyOnClick()
    {
        Application.LoadLevel("CreateGameMenu");
        //GameObject.Find("NetworkManager").GetComponent<MyNetworkManager>().StartGame();
    }

    //Called in create level scene to start the game
    public void CreateGame(Text matchName)
    {
        MyNetworkManager myNetwork = GameObject.Find("NetworkManager").GetComponent<MyNetworkManager>(); ;
        myNetwork.matchName = matchName.text;
        myNetwork.matchSize = 2;
        MyNetworkManager.singleton.matchName = matchName.text;
        myNetwork.StartGame(matchName.text);
    }

}
