using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RectTransform))]
public class HealthBar : MonoBehaviour
{
    //public GameObject player;
    private Health playerHealth;
	private RectTransform rectTrans;
	
	public void Start ()
	{
		rectTrans = GetComponent<RectTransform> ();
       
	}
	
	public void Update ()
    {
        if (!playerHealth)
        {
            //fix later
            playerHealth = GameObject.Find("PhysicalBody_working(Clone)").GetComponent<Health>();
        }
        else {
            transform.localScale = new Vector3(playerHealth.GetHealthPercentage(), 1, 1);//.x = playerHealth.GetHealthPercentage();
        }
	}

    public void SetHealth(Health newPlayerHealth)
    {
        playerHealth = newPlayerHealth;
    }
}