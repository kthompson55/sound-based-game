using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class ControllerKeyboard : MonoBehaviour 
{
    public KeyboardRow[] rows;
    public Color unselectedKeyColor;
    public Color unselectedKeyTextColor;
    public Color selectedKeyColor;
    public Color selectedKeyTextColor;
    public InputField editField;
    public AudioClip switchOptionClip;
    public AudioClip selectOptionClip;

    private AudioSource audio;
    private int currentRowIndex;
    private int currentKeyIndex;

    void Awake()
    {
        audio = GetComponent<AudioSource>();
        transform.position = new Vector3(transform.position.x, transform.position.y, -2); // top of the UIs
        currentRowIndex = 0;
        currentKeyIndex = 0;
    }
	
	// Update is called once per frame
	void Update () 
    {
        float yDirection = Input.GetButtonDown("Vertical") ? Input.GetAxis("Vertical") : 0;
        float xDirection = Input.GetButtonDown("Horizontal") ? Input.GetAxis("Horizontal") : 0;

        rows[currentRowIndex].buttons[currentKeyIndex].GetComponent<Image>().color = unselectedKeyColor;
        rows[currentRowIndex].buttons[currentKeyIndex].GetComponentInChildren<Text>().color = unselectedKeyTextColor;

        // move up a row on keyboard
        if (yDirection > 0 && currentRowIndex > 0)
        {
            currentRowIndex--;
            audio.PlayOneShot(switchOptionClip);
        }
        // move down a row on keyboard
        else if (yDirection < 0 && currentRowIndex < rows.Length - 1)
        {
            currentRowIndex++;
            audio.PlayOneShot(switchOptionClip);
        }
        // avoid flying off row into NullPointerExceptions
        if (currentKeyIndex > rows[currentRowIndex].buttons.Length - 1)
        {
            currentKeyIndex = rows[currentRowIndex].buttons.Length - 1;
        }

        // move left on keyboard
        if (xDirection > 0 && currentKeyIndex < rows[currentRowIndex].buttons.Length - 1)
        {
            currentKeyIndex++;
            audio.PlayOneShot(switchOptionClip);
        }
        // move right on keyboard
        else if (xDirection < 0 && currentKeyIndex > 0)
        {
            currentKeyIndex--;
            audio.PlayOneShot(switchOptionClip);
        }

        rows[currentRowIndex].buttons[currentKeyIndex].GetComponent<Image>().color = selectedKeyColor;
        rows[currentRowIndex].buttons[currentKeyIndex].GetComponentInChildren<Text>().color = selectedKeyTextColor;

        // check if pressed
        if (Input.GetButtonDown("Submit"))
        {
            audio.PlayOneShot(selectOptionClip);
            rows[currentRowIndex].buttons[currentKeyIndex].onClick.Invoke();
        }
    }
}
