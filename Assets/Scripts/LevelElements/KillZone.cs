using UnityEngine;
using System.Collections;

public class KillZone : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Health objectHealth = other.GetComponent<Health>();
            if (objectHealth != null)
            {
                objectHealth.currHealth = 0;
            }
        }

    }

}
