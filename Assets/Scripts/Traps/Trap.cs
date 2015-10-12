using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Animator))]
public class Trap : MonoBehaviour
{
    public TrapTrigger trigger;
	public int damage;
    public float coolDown;

    public void ResetTrap() {
        trigger.ResetTrigger();
    }
}
