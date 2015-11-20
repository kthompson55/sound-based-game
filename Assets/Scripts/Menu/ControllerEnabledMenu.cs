using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Canvas))]
public class ControllerEnabledMenu : MonoBehaviour 
{
    public Color unselectedButtonTextColor;
    public Color selectedButtonTextColor;
    
    private List<Button> menuButtons;
    private int currentButtonIndex;

	// Use this for initialization
	void Start () 
    {
        menuButtons = new List<Button>();
        for (int i = 0; i < transform.childCount; i++)
        {
            Button currentChildButton = transform.GetChild(i).GetComponent<Button>();
            if(currentChildButton)
            {
                menuButtons.Add(currentChildButton);
            }
        }

        currentButtonIndex = 0;
        menuButtons[0].GetComponentInChildren<Text>().color = selectedButtonTextColor;
	}
	
	// Update is called once per frame
	void Update () 
    {
        float direction = Input.GetButtonDown("Vertical") ? Input.GetAxis("Vertical") : 0;
        menuButtons[currentButtonIndex].GetComponentInChildren<Text>().color = unselectedButtonTextColor;
        // move up in menu
        if(direction > 0 && currentButtonIndex > 0)
        {
            currentButtonIndex--;
        }
        // move down in menu
        else if(direction < 0 && currentButtonIndex < menuButtons.Count - 1)
        {
            currentButtonIndex++;
        }
        menuButtons[currentButtonIndex].GetComponentInChildren<Text>().color = selectedButtonTextColor;

        // check if pressed
        if (Input.GetButtonDown("Submit"))
        {
            menuButtons[currentButtonIndex].onClick.Invoke();
        }
	}
}
