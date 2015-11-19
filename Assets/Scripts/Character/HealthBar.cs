using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(RectTransform))]
public class HealthBar : MonoBehaviour
{
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
            GameObject possibleHealth = GameObject.Find("PhysicalBody_working");
            // DEBUG scenes
            if(!possibleHealth)
            {
                possibleHealth = GameObject.Find("DebugPhysicalCharacter");
            }
            playerHealth = (possibleHealth != null) ? possibleHealth.GetComponent<Health>() : 
                GameObject.Find("PhysicalBody_working(Clone)").GetComponent<Health>();
        }
        else 
        {
            transform.localScale = new Vector3(playerHealth.GetHealthPercentage(), 1, 1);//.x = playerHealth.GetHealthPercentage();

        }
	}

    public void SetHealth(Health newPlayerHealth)
    {
        playerHealth = newPlayerHealth;
    }

    private void UpdateColor()
    {
        if(playerHealth.GetComponent<Health>().GetHealthPercentage() > .7f) // greater than 70%
        {
            GetComponent<Image>().color = Color.green;
        }
        else if(playerHealth.GetComponent<Health>().GetHealthPercentage() > .33f) // greater than 33%
        {
            GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            GetComponent<Image>().color = Color.red;
        }
    }
}