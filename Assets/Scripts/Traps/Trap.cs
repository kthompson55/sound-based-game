using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Animator))]
public class Trap : MonoBehaviour
{
	public int damage;
    private Animator anim;

    void Start() {
        anim = GetComponent<Animator>();
    }

    public void ResetTrap() {
        anim.SetBool("triggered", false);
    }
}
