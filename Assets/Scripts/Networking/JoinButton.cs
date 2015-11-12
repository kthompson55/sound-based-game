using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JoinButton : MonoBehaviour {

    public int matchLocation=-1;

    //Lobby manager join game button uses this
    public void join()
    {
        GameObject.Find("MultiplayerLobbyManager").GetComponent<LobbyManger>().JoinGame(matchLocation);
    }

}
