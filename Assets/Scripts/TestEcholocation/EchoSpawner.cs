﻿using UnityEngine;
using System.Collections;

public class EchoSpawner : MonoBehaviour {

    public Material echoMaterial = null;

    public float maxRadius = 10.0f;		
    public float currentRadius = 0.0f;	

    public float fadeDelay = 0.0f;			
    public float fadeRate = 1.0f;			
    public float echoSpeed = 9.0f;
    public Color echoColor = Color.white;

    private float dt = 0.0f;

    public float fade = 0.0f;

    private int echolLocation;

    private EchoManager listener;

    public bool Starts=false;

	// Use this for initialization
	void Start () {

        currentRadius = 0.0f;
        fade = 0.0f;
        echoMaterial.SetFloat("_DistanceFade", 1.0f);
        echoMaterial.SetColor("_MainColor" + echolLocation, echoColor);
        echoMaterial.SetFloat("_Radius" + echolLocation, currentRadius);
        echoMaterial.SetFloat("_Fade" + echolLocation, fade);

	}
	
	// Update is called once per frame
	void Update () {
        dt += Time.deltaTime;


        if (currentRadius >= maxRadius)
        {
            currentRadius = 0.0f;
            fade = 0.0f;
            echoMaterial.SetFloat("_Radius" + echolLocation, currentRadius);
            echoMaterial.SetFloat("_Fade" + echolLocation, fade);
            //echoMaterial.SetColor("_MainColor" + echolLocation, Color.white);

            if (listener != null)
            {
                listener.spotOpen(echolLocation);
                Destroy(this.gameObject);
            }
        }
        else
        {
            currentRadius += Time.deltaTime * echoSpeed;
            updateShader();
        }
        

	}

    public void subscribeToDeath(EchoManager manager)
    {
        listener = manager;
    }

    public void setEchoLocation(int location)
    {
        echolLocation = location;
    }

    public int getEchoLocation()
    {
        return echolLocation;
    }

    void updateShader()
    {
        if (dt > fadeDelay)
        {
            fade += Time.deltaTime * fadeRate;
        }
        echoMaterial.SetColor("_MainColor" + echolLocation, echoColor);
        echoMaterial.SetVector("_Position"+echolLocation, transform.position);
        echoMaterial.SetFloat("_Radius"+echolLocation, currentRadius);
        echoMaterial.SetFloat("_MaxRadius"+echolLocation, maxRadius);
        echoMaterial.SetFloat("_Fade" + echolLocation, fade);
        echoMaterial.SetFloat("_MaxFade", maxRadius/echoSpeed);

        if (echoColor.r != 0)
        {
            //Debug.Log(echoColor);
            //Debug.Log(echolLocation);
        }
    }
}
