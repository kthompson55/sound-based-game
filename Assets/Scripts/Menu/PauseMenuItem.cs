using UnityEngine;
using System.Collections;

public class PauseMenuItem : MonoBehaviour 
{
    private GameManager manager;

    void Awake()
    {
        manager = FindObjectOfType<GameManager>();
    }

    public void TogglePause()
    {
        manager.TogglePause();
    }

    public void ResumeGame()
    {
        manager.ExitPause();
    }

    public void PauseGame()
    {
        manager.EnterPause();
    }

    public void ReturnToMenu()
    {
        BreakNetworkConnection();
        Destroy(manager.gameObject); // avoid duplicate Network/Game Managers
        Application.LoadLevel("Main_Menu");
    }

    public void ExitGame()
    {
        BreakNetworkConnection();
        Application.Quit();
    }

    void BreakNetworkConnection()
    {
        // TODO: Drop network connection
    }
}
