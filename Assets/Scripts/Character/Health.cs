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
    public Color damageFlashColor;
    public float fadeSpeed = 2;
	public float invincibilityTime = 0.5f;

    private Image damageFlashImage;
    private bool hit;
	private long hitTime;
    private bool flashing = false;
	
	void Start ()
	{
		currHealth = maxHealth;
		collisionBox = GetComponent<BoxCollider> ();
		hit = false;
        GameObject can = GameObject.Find("Health");
        if (can) 
        { 
            for(int i = 0; i < can.transform.childCount; i++)
            {
                if(can.transform.GetChild(i).name == "RedFlash")
                {
                    damageFlashImage = can.transform.GetChild(i).GetComponent<Image>();
                }
            }
            Image img = can.GetComponentInChildren<Image>();
            img.GetComponent<HealthBar>().SetHealth(this);
        }
        else 
        {
            Debug.Log("No canvas, cannot display health");
        }
	}

    void Update()
    {
        Debug.Log(damageFlashImage.name);
        // check if invincible
        if (hit)
        {
            if ((System.DateTime.Now.Ticks * 10000) - hitTime > (invincibilityTime / 1000))
            {
                hit = false;
            }
        }
        // fade in red flash
        if(flashing)
        {
            FadeIn();
        }
        // fade out red flash
        else
        {
            FadeOut();
        }
        Debug.Log(damageFlashImage.color);

        // return to main menu on death
        if(currHealth <= 0)
        {
            //fi
            MyNetworkManager.singleton.StopHost();
            Application.LoadLevel("MainMenu");
        }
    }
	
	void OnCollisionEnter(Collision col)
	{
		if ((col.gameObject.tag == "Enemy" || col.gameObject.tag == "Trap") && !hit) 
        {
            currHealth -= col.gameObject.tag == "Enemy" ? col.gameObject.GetComponent<Enemy> ().damage : col.gameObject.GetComponent<Trap> ().damage;
            currHealth = Mathf.Clamp(currHealth, 0, maxHealth);
            hit = true;
			hitTime = System.DateTime.Now.Ticks * 10000;
            StartCoroutine("FlashRedScreen");
		}
	}
	
	public float GetHealthPercentage()
	{
        return ((float)currHealth) / ((float)maxHealth);
	}

    IEnumerator FlashRedScreen()
    {
        flashing = true;
        yield return new WaitForSeconds(invincibilityTime);
        flashing = false;
    }

    // fades damage color into visibility
    void FadeIn()
    {
        damageFlashImage.color = Color.Lerp(damageFlashImage.color, damageFlashColor, fadeSpeed * Time.deltaTime);
    }

    // fades damage color to transparent
    void FadeOut()
    {
        damageFlashImage.color = Color.Lerp(damageFlashImage.color, Color.clear, fadeSpeed * Time.deltaTime);
    }
}
