using UnityEngine;
using System.Collections;

public class MySoundScript : MonoBehaviour {
    public float time;
    public float endTime;

	// Use this for initialization
	void Start () {
        time = 0;
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
	}
}
