using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(AudioSource))]
public class ControllerEnabledMenu : MonoBehaviour 
{
    public Color unselectedButtonTextColor;
    public Color selectedButtonTextColor;
    public AudioClip switchOptionClip;
    public AudioClip selectOptionClip;

    private AudioSource audio;
    private List<Button> menuButtons;
    private int currentButtonIndex;
    private bool locked;

	// Use this for initialization
	void Start () 
    {
        audio = GetComponent<AudioSource>();
        menuButtons = new List<Button>();
        locked = false;
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
        if(!locked)
        {
            float direction = Input.GetButtonDown("Vertical") ? Input.GetAxis("Vertical") : 0;
            menuButtons[currentButtonIndex].GetComponentInChildren<Text>().color = unselectedButtonTextColor;
            // move up in menu
            if (direction > 0 && currentButtonIndex > 0)
            {
                currentButtonIndex--;
                audio.PlayOneShot(switchOptionClip);
            }
            // move down in menu
            else if (direction < 0 && currentButtonIndex < menuButtons.Count - 1)
            {
                currentButtonIndex++;
                audio.PlayOneShot(switchOptionClip);
            }
            menuButtons[currentButtonIndex].GetComponentInChildren<Text>().color = selectedButtonTextColor;

            // check if pressed
            if (Input.GetButtonDown("Submit"))
            {
                audio.PlayOneShot(selectOptionClip);
                locked = true;
            }
        }
        else if(!audio.isPlaying)
        {
            menuButtons[currentButtonIndex].onClick.Invoke();            
        }
	}
}
