using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour
{
	public int damage;
	public float cooldown;
    private bool triggered;

    void Start() {
        triggered = false;
    }

    void Update() {
        if (triggered) {
            //trigger an animation later
            triggered = false;
        }
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "player") {
            triggered = true;
        }
    }
}
