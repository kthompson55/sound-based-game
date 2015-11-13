using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
    public Canvas pauseMenu;
    public bool pausable;

    void Awake()
    {
        // disables cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        pausable = false; // starts on main menu, so cannot pause
        pauseMenu.transform.position = new Vector3(pauseMenu.transform.position.x, pauseMenu.transform.position.y, -1); // forces in front of any overlays
        pauseMenu.enabled = false;
    }

    void Update()
    {
        if(pausable && Input.GetButtonDown("Pause"))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        pauseMenu.enabled = !pauseMenu.enabled;
    }

    public void EnterPause()
    {
        pauseMenu.enabled = true;
    }

    public void ExitPause()
    {
        pauseMenu.enabled = false;
    }
}
