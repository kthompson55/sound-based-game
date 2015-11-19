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
    public Image damageSplash;
    public Color flashColor;
    public float flashSpeed;
	
	public float invincibilityTime = 0.5f;
	private bool hit;
	private long hitTime;
    private bool flashOn;
    private bool flashOff;
	
	void Start ()
	{
		currHealth = maxHealth;
		collisionBox = GetComponent<BoxCollider> ();
		hit = false;
        flashOn = false;
        flashOff = false;
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
        if(flashOn)
        {
            ShowDamageFlash();
        }
        else if(flashOff)
        {
            HideDamageFlash();
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
            currHealth -= col.gameObject.tag == "Enemy" ? col.gameObject.GetComponent<Enemy> ().damage : col.gameObject.GetComponent<Trap> ().damage;
            currHealth = Mathf.Clamp(currHealth, 0, maxHealth);
            hit = true;
			hitTime = System.DateTime.Now.Ticks * 10000;
            flashOn = true;
		}
	}
	
	public float GetHealthPercentage ()
	{
        return ((float)currHealth) / ((float)maxHealth);
	}

    void ShowDamageFlash()
    {
        damageSplash.color = Color.Lerp(damageSplash.color, flashColor, flashSpeed);
        if(CompareColors(damageSplash.color, flashColor))
        {
            flashOn = false;
            flashOff = true;
        }
    }

    void HideDamageFlash()
    {
        damageSplash.color = Color.Lerp(damageSplash.color, Color.clear, flashSpeed);
        if(CompareColors(damageSplash.color, Color.clear))
        {
            flashOff = false;
        }
    }

    private bool CompareColors(Color first, Color second)
    {
        bool redValue = Mathf.Abs(first.r - second.r) < .05f;
        bool greenValue = Mathf.Abs(first.g - second.g) < .05f;
        bool blueValue = Mathf.Abs(first.b - second.b) < .05f;
        bool alphaValue = Mathf.Abs(first.a - second.a) < .1f;

        return redValue && greenValue && blueValue && alphaValue;
    }
}
