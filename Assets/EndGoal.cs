using UnityEngine;
using System.Collections;

public class EndGoal : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        ChangeScene("MainMenu");
    }


    void ChangeScene(string toScene) {
        Application.LoadLevel(toScene);
    }
}
