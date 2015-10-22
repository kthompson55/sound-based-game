using UnityEngine;
using System.Collections;

public class StartGameUI : MonoBehaviour {


    public void MyOnClick()
    {
        GameObject.Find("NetworkManager").GetComponent<MyNetworkManager>().StartGame();
    }

}
