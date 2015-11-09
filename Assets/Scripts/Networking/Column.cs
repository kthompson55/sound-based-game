using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Column : MonoBehaviour {

    ArrayList text = new ArrayList();
    ArrayList button = new ArrayList();
    public Button fix;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
      
	}

    public void AddRow(int location)
    {
        Text temp = null;
        foreach(Text i in Canvas.FindObjectsOfType<Text>()){
            if (i.name.Equals("MatchNames"))
            {
                   temp = i;
            }
        }
        Text newText=null;
        if(temp!=null&&temp.transform.childCount!=0){
            newText = GameObject.Instantiate(temp.transform.GetChild(0).gameObject.GetComponent<Text>());
        }
        else{
            newText = GameObject.Instantiate(temp);   
        }
        newText.name = "Room1";
        newText.text = MyNetworkManager.singleton.matches[location].name;
        newText.rectTransform.parent=temp.rectTransform;
        newText.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        text.Add(newText);
        newText.transform.localPosition = new Vector3(-10.0f, -20.0f*text.Count, 0.0f);


        Button newButton = GameObject.Instantiate(fix);
        newButton.enabled = true;
        newButton.gameObject.SetActive(true);
        newButton.gameObject.GetComponent<JoinButton>().matchLocation=button.Count;
        newButton.transform.parent = temp.rectTransform;
        newButton.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        button.Add(newButton);
        newButton.transform.localPosition = new Vector3(60.0f, -20.0f * button.Count, 0.0f);
        newButton.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Join";

    }
}
