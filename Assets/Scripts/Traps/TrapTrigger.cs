using UnityEngine;
using System.Collections;

public class TrapTrigger : MonoBehaviour
{
    private bool triggered;
    private long triggeredTime;
    public Trap attached;
    private Animator trapAnimator;

    void Start() {
        triggered = false;
        trapAnimator = attached.GetComponent<Animator>();
        if (!trapAnimator) {
            Debug.LogError("No Trap Animator for this trap! Trap name: " + gameObject.name);
        }
    }

    void OnTriggerEnter(Collider col) {
        if (!triggered && col.gameObject.tag.ToLower() == "player")
        {
            Debug.Log("Trap triggered!!");
            triggered = true;
            trapAnimator.SetBool("triggered", triggered);
            triggeredTime = System.DateTime.Now.Ticks * 10000;
        }
    }

    public void ResetTrigger() {
        triggered = false;
        trapAnimator.SetBool("triggered", triggered);
    }

    public bool IsTriggered() {
        return triggered;
    }

    public long GetTriggeredTime() {
        return triggeredTime;
    }
}
