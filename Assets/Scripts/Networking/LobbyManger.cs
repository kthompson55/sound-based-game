using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LobbyManger : MonoBehaviour {

    MyNetworkManager mnm;
    int matchsCount = 0;
    public Column matchNameColumn;

	// Use this for initialization
	void Start () {
	    
	}

    //JoinButton calls this, goes to Networkmanager
    public void JoinGame(int location)
    {
        GameObject.Find("NetworkManager").GetComponent<MyNetworkManager>().JoinGame(location);

    }
	
	// Update is called once per frame
	void Update () {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (mnm == null)
        {
            mnm = GameObject.Find("NetworkManager").GetComponent<MyNetworkManager>();
        }

        if (Input.GetKeyDown("k"))
        {
            if (matchNameColumn != null)
            {
                //matchNameColumn.AddRow(0);
            }
        }

        if (mnm!=null&&MyNetworkManager.singleton.matchMaker!=null)
        {
            MyNetworkManager.singleton.matchMaker.ListMatches(0, 20, "", MyNetworkManager.singleton.OnMatchList);
            if (mnm.AreMatches())
            {
                if (matchsCount < mnm.matches.Count)
                {
                    for (int run = matchsCount; run < mnm.matches.Count; run++)
                    {
                        matchNameColumn.AddRow(run);
                    }
                    matchsCount = mnm.matches.Count;
                }
            }
        }
	}
}
