using UnityEngine;
using System.Collections;

public class EchoSpawner : MonoBehaviour {

    public Material echoMaterial = null;

    public float maxRadius = 10.0f;		
    public float currentRadius = 0.0f;	

    public float fadeDelay = 0.0f;			
    public float fadeRate = 1.0f;			
    public float echoSpeed = 9.0f;			

    private float dt = 0.0f;

    public float fade = 0.0f;

	// Use this for initialization
	void Start () {

        echoMaterial.SetFloat("_DistanceFade", 1.0f);
        

	}
	
	// Update is called once per frame
	void Update () {
        dt += Time.deltaTime;

        if (currentRadius >= maxRadius)
        {
            currentRadius = 0.0f;
            fade = 0.0f;
            Destroy(this.gameObject);
        }
        else
        {
            currentRadius += Time.deltaTime * echoSpeed;
            updateShader();
        }

	}

    void updateShader()
    {
        if (dt > fadeDelay)
        {
            fade += Time.deltaTime * fadeRate;
        }


        echoMaterial.SetVector("_Position", transform.position);
        echoMaterial.SetFloat("_Radius", currentRadius);
        echoMaterial.SetFloat("_MaxRadius", maxRadius);
        echoMaterial.SetFloat("_Fade", fade);
        echoMaterial.SetFloat("_MaxFade", maxRadius/echoSpeed);
    }
}
