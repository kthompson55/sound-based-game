using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RectTransform))]
public class HealthBar : MonoBehaviour
{
    public GameObject player;
    public Health playerHealth;
	private RectTransform rectTrans;
	
	public void Start ()
	{
		rectTrans = GetComponent<RectTransform> ();
        if (!player)
        {
            Debug.LogError("Need to attach a ref to player");
        }
        else {
            playerHealth = player.GetComponent<Health>();
        }
	}
	
	public void Update ()
	{
        transform.localScale = new Vector3(playerHealth.GetHealthPercentage(), 1, 1);//.x = playerHealth.GetHealthPercentage();
	}

    public void SetHealth(Health newPlayerHealth)
    {
        playerHealth = newPlayerHealth;
    }
}