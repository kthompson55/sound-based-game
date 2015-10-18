using UnityEngine;
using System.Collections;

public class KillZone : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collide with object");
        Health objectHealth = other.GetComponent<Health>();
        if (objectHealth != null)
        {
            objectHealth.currHealth = 0;
        }

    }

}
