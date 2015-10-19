using UnityEngine;
using System.Collections;

public class TrapTrigger : MonoBehaviour
{
    private bool triggered;
    private long triggeredTime;
    public Trap[] attachedTraps;
    private Animator[] trapAnimators;
    public float cooldown;

    void Start() {
        triggered = false;

        trapAnimators = new Animator[attachedTraps.Length];
        for(int i = 0; i < attachedTraps.Length; i ++){
            trapAnimators[i] = attachedTraps[i].GetComponent<Animator>();
            if (!trapAnimators[i])
            {
                Debug.LogError("No Trap Animator for this trap! Trap name: " + attachedTraps[i].name);
            }
        }
        
    }

    void OnTriggerEnter(Collider col) {
        if (!triggered && col.gameObject.tag.ToLower() == "player")
        {
            triggered = true;
            foreach (Animator anim in trapAnimators) {
                anim.SetBool("triggered", triggered);
            }
            triggeredTime = System.DateTime.Now.Ticks * 10000;
        }
    }

    public void ResetTrigger() {
        triggered = false;
        foreach (Animator anim in trapAnimators)
        {
            anim.SetBool("triggered", triggered);
        }
    }

    public bool IsTriggered() {
        return triggered;
    }

    public long GetTriggeredTime() {
        return triggeredTime;
    }

    void Update() {
        if (triggered) {
            if ((System.DateTime.Now.Ticks * 10000) - triggeredTime > (cooldown / 1000)) {
                triggered = false;
            }
        }
    }

    //when the player enters the trigger, sets the triggered animator variable for all of the attached traps
    //traps then 

}
