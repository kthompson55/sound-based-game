using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Health : MonoBehaviour
{
	public int maxHealth;
	private int currHealth;
	private BoxCollider collisionBox;
	
	public float invincibilityTime = 0.5f;
	private bool hit;
	private long hitTime;
	
	void Start ()
	{
		currHealth = maxHealth;
		collisionBox = GetComponent<BoxCollider> ();
		hit = false;
	}
	
	void OnCollisionEnter (Collision col)
	{
		if ((col.gameObject.tag == "enemy" || col.gameObject.tag == "trap") && !hit) {
			currHealth -= col.gameObject.tag == "enemy" ? col.gameObject.GetComponent<Enemy> ().damage : col.gameObject.GetComponent<Trap> ().damage;
			hit = true;
			hitTime = System.DateTime.Now.Ticks * 10000;
		}
	}
	
	void Update ()
	{
		if (hit) {
			if ((System.DateTime.Now.Ticks * 10000) - hitTime > (invincibilityTime / 1000)) {
				hit = false;
			}
		}
	}
}
