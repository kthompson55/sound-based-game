using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartGameUI : MonoBehaviour {


    public void MyOnClick()
    {
        Application.LoadLevel("CreateGameMenu");
        //GameObject.Find("NetworkManager").GetComponent<MyNetworkManager>().StartGame();
    }

    public void CreateGame(Text matchName)
    {
        MyNetworkManager temp=GameObject.Find("NetworkManager").GetComponent<MyNetworkManager>();
        temp.matchName = matchName.text;
        temp.matchSize = 2;
        //MyNetworkManager.singleton.matchName = matchName.text;
        temp.StartGame();
    }

}
