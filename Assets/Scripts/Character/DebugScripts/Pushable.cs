using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Pushable : MonoBehaviour {
    Vector3 pushDirection;
    float pushTime;
    float pushSpeed = 0.5f;
    bool pushing = false;

    void Update()
    {
        if (pushing)
        {
            Debug.Log("Pushing is true");
            //transform.position = Vector3.Lerp(transform.position, transform.position + pushDirection, pushSpeed);
            if (pushTime < System.DateTime.Now.Millisecond)
            {
                Debug.Log("Stop moving");
                pushing = false;
            }
        }

    }
    void OnTriggerEnter(Collider spirit)
    {
        if (spirit.GetComponent<SpiritualBody>() != null)
        {
            Debug.Log("Pushing");
            pushTime = System.DateTime.Now.AddMilliseconds(1000).Millisecond;
            pushDirection = Vector3.Normalize(transform.position - spirit.transform.position);
            pushDirection = new Vector3(pushDirection.x, 0, pushDirection.z);

            Rigidbody r = GetComponent<Rigidbody>();
            r.AddForce(pushDirection * 425);

            pushing = true;
        }
    }
}
