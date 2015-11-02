using UnityEngine;
using System.Collections;

public class EndGoal : MonoBehaviour 
{
    public string nextLevel;

    void OnTriggerEnter(Collider col)
    {
        Application.LoadLevel(nextLevel);
    }
}
