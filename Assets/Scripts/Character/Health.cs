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
        if(!hpBar)
        {
            hpBar = FindObjectOfType<HealthBar>();
        }
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

    public void DamagePlayer(int numDamage)
    {
        currHealth -= numDamage;
    }

    //OnCollisionEnter is the right one. The player isn't a trigger so when the enemy collides with the player they don't trigger the OnTriggerEnter function.
    //void OnCollisionEnter(Collision col)
    //{
    //    MonoBehaviour damageSource = GetDamageSource(col.gameObject.transform);
    //    if (damageSource != null && !hit)
    //    {
    //        currHealth -= col.gameObject.tag == "Enemy" ? col.gameObject.GetComponent<Enemy>().damage : col.gameObject.GetComponent<Trap>().damage;
    //        currHealth = Mathf.Clamp(currHealth, 0, maxHealth);
    //        hit = true;
    //        hitTime = System.DateTime.Now.Ticks * 10000;
    //        hpBar.StartFlash();
    //    }
    //}
	
	void OnTriggerEnter (Collider col)
	{
        MonoBehaviour damageSource = GetDamageSource(col.gameObject.transform);
        if (damageSource != null && !hit) 
        {
            currHealth -= col.gameObject.tag == "Enemy" ? col.gameObject.GetComponent<Enemy> ().damage : col.gameObject.GetComponent<Trap> ().damage;
            currHealth = Mathf.Clamp(currHealth, 0, maxHealth);
            hit = true;
			hitTime = System.DateTime.Now.Ticks * 10000;
            hpBar.StartFlash();
		}
	}
	
	public float GetHealthPercentage ()
	{
        return ((float)currHealth) / ((float)maxHealth);
	}

    private MonoBehaviour GetDamageSource(Transform transform)
    {
        MonoBehaviour behaviour = null;
        GameObject damageObject = null;
        string tag = transform.gameObject.tag;
        bool isDamageSource = tag == "Enemy" || tag == "Trap";
        if(!isDamageSource)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                tag = transform.GetChild(i).gameObject.tag;
                isDamageSource = tag == "Enemy" || tag == "Trap";
                if (isDamageSource)
                {
                    damageObject = transform.GetChild(i).gameObject;
                    break;
                }
            }
        }
        else
        {
            damageObject = transform.gameObject;
        }

        if(isDamageSource)
        {
            if(tag == "Enemy")
            {
                behaviour = damageObject.GetComponent<Enemy>();
            }
            else if(tag == "Trap")
            {
                behaviour = damageObject.GetComponent<Trap>();
            }
        }

        return behaviour;
    }

}
