using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KeyboardRow : MonoBehaviour 
{
    public Button[] buttons;

    void Awake()
    {
        buttons = new Button[transform.childCount];
        for(int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Button>())
            {
                buttons[i] = transform.GetChild(i).GetComponent<Button>();
            }
        }
    }
}
