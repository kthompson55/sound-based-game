using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnterKey : MonoBehaviour, KeyboardKey
{
    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(delegate { HandlePressEvent(); });
    }

    public void HandlePressEvent()
    {
        Transform parent = transform.parent;
        while (!parent.GetComponent<Canvas>())
        {
            parent = parent.transform.parent;
        }

        Canvas parentCanvas = parent.gameObject.GetComponent<Canvas>();
        InputField fieldToModify = null;
        for (int i = 0; i < parentCanvas.transform.childCount; i++)
        {
            InputField field = parentCanvas.transform.GetChild(i).GetComponent<InputField>();
            if (field)
            {
                fieldToModify = field;
                break;
            }
        }

        string matchName = fieldToModify.text;
        if(matchName.Length > 0)
        {
            MyNetworkManager myNetwork = GameObject.Find("NetworkManager").GetComponent<MyNetworkManager>(); ;
            myNetwork.matchName = matchName;
            myNetwork.matchSize = 2;
            MyNetworkManager.singleton.matchName = matchName;

            myNetwork.StartGame(matchName);
        }
        else
        {
            fieldToModify.image.color = Color.red;
        }
    }
}
