using UnityEngine;
using System.Collections;

public class EnemyFieldOfView : MonoBehaviour {

    private Enemy attachedEnemy;

	void Start () {
        attachedEnemy = GetComponentInParent<Enemy>();
        if (!attachedEnemy) {
            Debug.LogError("This field of view doesn't have a parent with an enemy script; fov name: " + name);
        }
	}

    void OnTriggerEnter(Collider col)
    {
        UpdatePlayerLastKnownPosition(col);
    }

    void OnTriggerStay(Collider col) {
        UpdatePlayerLastKnownPosition(col);
    }

    void UpdatePlayerLastKnownPosition(Collider col)
    {
        if ( col.gameObject.CompareTag("Player") ){
             Debug.Log("Enemy sees player!!");
             attachedEnemy.ChasePlayer(col);
        }
    }
}
