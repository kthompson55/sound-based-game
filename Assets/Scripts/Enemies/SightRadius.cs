using UnityEngine;
using System.Collections;


public class SightRadius : MonoBehaviour {

    EnemyStateMachine sm;

    void Start()
    {
        sm = gameObject.GetComponentInParent<Enemy>().stateMachine;
    }


    void Update()
    {
        if (sm == null)
        {
            sm = GetComponentInParent<Enemy>().stateMachine;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Body"))
        {
            //sm = null;
            sm.lastKnownLocation = other.gameObject.transform.position;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name.Contains("Body"))
        {
            sm.lastKnownLocation = other.gameObject.transform.position;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("Body"))
        {
            sm.lastKnownLocation = other.gameObject.transform.position;
        }
    }

    

}
