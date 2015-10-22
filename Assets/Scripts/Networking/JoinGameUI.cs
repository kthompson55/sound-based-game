using UnityEngine;
using System.Collections;

public class JoinGameUI : MonoBehaviour {


    public void MyOnClick()
    {
        GameObject.Find("NetworkManager").GetComponent<MyNetworkManager>().JoinGame();
    }
}
