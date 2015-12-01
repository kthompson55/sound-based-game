using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class AudioSpawn : MonoBehaviour {
    public AudioClip clip;
    public Transform position;
    public Transform SecondPosition;
    public AudioManagerScript manage;

	// Use this for initialization
	void Start () {
        manage = GameObject.Find("AudioManager").GetComponent<AudioManagerScript>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void spawn(float TimeToEnd = 10.0f)
    {
        if (manage == null)
        {
            manage = GameObject.Find("AudioManager").GetComponent<AudioManagerScript>();
        }
        manage.Spawn(clip, position, TimeToEnd);
    }
}
