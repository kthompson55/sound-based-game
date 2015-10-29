using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Pushable : MonoBehaviour {
    Vector3 pushDirection;
    float pushTime;
    bool pushing = false;

    void Update()
    {
        if (pushing)
        {
            Debug.Log("Pushing is true");
            pushing = false;
            transform.position += pushDirection;
            if ((System.DateTime.Now.Ticks * 10000) - pushTime > (50000 / 1000))
            {
                Debug.Log("Stop moving");
            }
        }

    }
    void OnTriggerEnter(Collider spirit)
    {
        if (spirit.GetComponent<SpiritualBody>() != null)
        {
            Debug.Log("Pushing");
            pushTime = System.DateTime.Now.Ticks * 10000;
            pushDirection = Vector3.Normalize(transform.position - spirit.transform.position);
            pushing = true;
        }
    }
}
