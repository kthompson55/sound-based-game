using UnityEngine;
using System.Collections;

public class EnemyFieldOfView : MonoBehaviour {

    private EnemyStateMachine sm;

	void Start () {
        sm = GetComponentInParent<Enemy>().stateMachine;

	}

    void Update()
    {
        if (sm == null)
        {
            sm = GetComponentInParent<Enemy>().stateMachine;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name.Contains("Body"))
        {
            sm.seeAPlayer = true;

        }
    }

    void OnTriggerStay(Collider col) {
        if (col.gameObject.name.Contains("Body"))
        {
            sm.seeAPlayer = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("Body"))
        {
            sm.seeAPlayer = false;
        }
    }
}
