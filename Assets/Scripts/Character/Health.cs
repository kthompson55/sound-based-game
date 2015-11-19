using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Health : MonoBehaviour
{
	public int maxHealth;
	public int currHealth;
    private BoxCollider collisionBox;
    public HealthBar hpBar;
	
	public float invincibilityTime = 0.5f;
	private bool hit;
	private long hitTime;
	
	void Start ()
	{
		currHealth = maxHealth;
		collisionBox = GetComponent<BoxCollider> ();
		hit = false;
        hpBar.SetHealth(GetComponent<Health>());
	}

    void Update()
    {
        //Debug.Log(currHealth);
        if (hit)
        {
            if ((System.DateTime.Now.Ticks * 10000) - hitTime > (invincibilityTime / 1000))
            {
                hit = false;
            }
        }
        if(currHealth <= 0)
        {
            //finish game
            Debug.Log("End Game!");
            MyNetworkManager.singleton.StopHost();
            MyNetworkManager.singleton.StopServer();
            MyNetworkManager.singleton.StopMatchMaker();
            //MyNetworkManager.singleton.StopClient();
            Application.LoadLevel("Main_Menu");
        }
    }
	
	void OnTriggerEnter (Collider col)
	{
		if ((col.gameObject.tag == "Enemy" || col.gameObject.tag == "Trap") && !hit) {
            Debug.Log("ouch");
            currHealth -= col.gameObject.tag == "Enemy" ? col.gameObject.GetComponent<Enemy> ().damage : col.gameObject.GetComponent<Trap> ().damage;
            currHealth = Mathf.Clamp(currHealth, 0, maxHealth);
            hit = true;
			hitTime = System.DateTime.Now.Ticks * 10000;
		}
	}
	
	public float GetHealthPercentage ()
	{
        return ((float)currHealth) / ((float)maxHealth);
	}
}
