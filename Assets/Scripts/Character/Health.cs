using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Health : MonoBehaviour
{
    public HealthBar hpBar;
    public SkinnedMeshRenderer characterMesh;
    public Material opaqueMaterial;
    public Material transparentMaterial;
    public float invincibilityTime = 0.5f;
    public int maxHealth;
    public int currHealth;
    
    private BoxCollider collisionBox;
	private bool hit;
	
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
        Transform currentObject = col.gameObject.transform;
        MonoBehaviour damageSource = null;
        do
        {
            damageSource = GetDamageSource(currentObject);
            if (damageSource == null && currentObject.parent != null)
            {
                currentObject = currentObject.parent;
            }
        }
        while (damageSource == null && currentObject.transform.parent != null && currentObject.transform.parent.tag == "Enemy");

        if (damageSource != null && !hit) 
        {
            currHealth -= currentObject.gameObject.tag == "Enemy" ? currentObject.gameObject.GetComponent<Enemy>().damage : currentObject.gameObject.GetComponent<Trap>().damage;
            currHealth = Mathf.Clamp(currHealth, 0, maxHealth);
            StartCoroutine(GotHit());
            hpBar.StartFlash();
		}
	}

    IEnumerator GotHit()
    {
        hit = true;
        StartCoroutine(TurnTransparent());
        yield return new WaitForSeconds(invincibilityTime);
        hit = false;
    }

    IEnumerator TurnTransparent()
    {
        characterMesh.material = opaqueMaterial;
        yield return new WaitForSeconds(invincibilityTime / 2);
        characterMesh.material = transparentMaterial;
        yield return new WaitForSeconds(invincibilityTime / 2);
        characterMesh.material = opaqueMaterial;
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
