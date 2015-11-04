using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartGameUI : MonoBehaviour {


    public void MyOnClick()
    {   
        GameObject.Find("NetworkManager").GetComponent<MyNetworkManager>().StartGame();
    }

    public void CreateGame(Text matchName)
    {
        MyNetworkManager temp=GameObject.Find("NetworkManager").GetComponent<MyNetworkManager>();
        temp.matchName = matchName.text;
        temp.matchSize = 2;
        temp.StartGame();
    }

}
