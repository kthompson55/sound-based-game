using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(RectTransform))]
public class HealthBar : MonoBehaviour
{
    public Image damageSplash;
    public Color flashColor;
    public float flashSpeed;
    
    private Health playerHealth;
    private bool flashOn;
    private bool flashOff;
	
	public void Start ()
	{
        flashOn = false;
        flashOff = false;
	}
	
	public void Update ()
    {
        if (playerHealth)
        {
            transform.localScale = new Vector3(playerHealth.GetHealthPercentage(), 1, 1);//.x = playerHealth.GetHealthPercentage();
            UpdateColor();

            if (flashOn)
            {
                ShowDamageFlash();
            }
            else if (flashOff)
            {
                HideDamageFlash();
            }
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
            GetComponent<Image>().color = new Color(.129f, .553f, .114f, 1);
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

    public void StartFlash()
    {
        flashOn = true;
    }

    void ShowDamageFlash()
    {
        damageSplash.color = Color.Lerp(damageSplash.color, flashColor, flashSpeed);
        if (CompareColors(damageSplash.color, flashColor))
        {
            flashOn = false;
            flashOff = true;
        }
    }

    void HideDamageFlash()
    {
        damageSplash.color = Color.Lerp(damageSplash.color, Color.clear, flashSpeed);
        if (CompareColors(damageSplash.color, Color.clear))
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