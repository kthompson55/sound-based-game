using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSpawn))]
public class TimedPlay : MonoBehaviour {
    private float time;
    public float timeToSpawn;
    public float timeToPlay;

	// Use this for initialization
	void Start () {
        time = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time > timeToSpawn)
        {
            time = 0.0f;
            GetComponent<AudioSpawn>().spawn(timeToPlay);
        }
	}
}
