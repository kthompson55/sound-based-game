using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManagerScript : MonoBehaviour {
    public Camera Physical;
    public Camera Spiritual;
    public GameObject mySoundPrefab;
    public List<GameObject> soundList = new List<GameObject>(20);

	void Start () {
    }

	void Update () {
        List<GameObject> removeableList = new List<GameObject>();
        foreach (GameObject s in soundList)
        {
            MySoundScript script = s.GetComponent<MySoundScript>();
            if (script.getTime() > script.endTime)
            {
                removeableList.Add(s);
            }
        }
        foreach (GameObject s in removeableList)
        {
            soundList.Remove(s);
            Destroy(s);
        }
    }

    public void Spawn(AudioClip clip, Transform position, float timeToPlay)
    {
        if (Physical.enabled)
        {
            physicalSpawn(position, clip, timeToPlay);
        }
        else if (Spiritual.enabled)
        {
            spiritSpawn(position, clip, timeToPlay);
        }
    }

    private void spiritSpawn(Transform position, AudioClip clip, float timeToPlay)
    {
        Debug.Log("Spirit Spawn");
        GameObject sound = Instantiate(mySoundPrefab);
        AudioSource soundSource = sound.AddComponent<AudioSource>();
        soundSource.transform.position = position.position;
        soundSource.spatialBlend = 1.0f;
        soundSource.rolloffMode = AudioRolloffMode.Linear;
        soundSource.maxDistance = 100.0f;
        soundSource.PlayOneShot(clip);
        soundList.Add(sound);
    }

    private void physicalSpawn(Transform position, AudioClip clip, float timeToPlay)
    {
        Debug.Log("Physical Spawn");

        GameObject sound = Instantiate(mySoundPrefab);

        AudioSource soundSource = sound.GetComponent<AudioSource>();

        //Cave like echos
        AudioReverbFilter filter = sound.AddComponent<AudioReverbFilter>();
        filter.reverbPreset = AudioReverbPreset.Cave;

        soundSource.transform.position = position.position;
        soundSource.spatialBlend = 1.0f;
        soundSource.rolloffMode = AudioRolloffMode.Linear;
        soundSource.maxDistance = 5.0f;
        soundSource.volume = 0.25f;
        soundSource.clip = clip;
        soundSource.Play();
        //soundSource.PlayOneShot(clip);
        soundList.Add(sound);
    }


}
