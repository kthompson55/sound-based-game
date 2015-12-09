using UnityEngine;
using System.Collections;

public class TrapTrigger : MonoBehaviour
{
    private bool triggered;
    public Trap[] attachedTraps;
    private Animator[] trapAnimators;
    public float cooldown;
    public bool stuck;
    public float timer = 0;

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
        if (!triggered && IsPlayer(col))
        {
            ActivateTrigger();
        }else if(col.gameObject.tag.Equals("Block")){
            Stuck();
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (IsPlayer(col))
        {
            timer = 0.0f;
            //ActivateTrigger();
        }else if (!stuck & col.gameObject.tag.Equals("Block"))
        {
            Stuck();
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (!triggered && IsPlayer(col))
        {
            ResetTrigger();
        }
        else if (!stuck & col.gameObject.tag.Equals("Block"))
        {
            UnStuck();
        }

    }

    private bool IsPlayer(Collider col) {
        return col.gameObject.tag.ToLower() == "player";
    }

    private void ActivateTrigger() {
        triggered = true;
        foreach (Animator anim in trapAnimators)
        {
            anim.SetBool("triggered", triggered);
        }
    }

    public void ResetTrigger() {
        triggered = false;

        timer = 0;
        foreach (Animator anim in trapAnimators)
        {
            anim.SetBool("triggered", triggered);
        }
    }

    public bool IsTriggered() {
        return triggered;
    }

    public void Stuck()
    {
        foreach (Animator anim in trapAnimators)
        {
            anim.SetBool("stuck", true);
        }
    }

    public void UnStuck()
    {
        foreach (Animator anim in trapAnimators)
        {
            anim.SetBool("stuck", false);
        }
    }
   
    void Update() {
        if (triggered) {
            timer += Time.deltaTime;
            if (timer > cooldown) {
                ResetTrigger();
            }
        }
    }

    //when the player enters the trigger, sets the triggered animator variable for all of the attached traps
    //traps then 

}
