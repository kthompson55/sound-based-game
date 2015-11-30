using UnityEngine;
using System.Collections;

public class MySoundScript : MonoBehaviour {
    private float time;
    public float endTime;

	// Use this for initialization
	void Start () {
        time = 0;
	}

    public float getTime()
    {
        return time;
    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
	}
}
