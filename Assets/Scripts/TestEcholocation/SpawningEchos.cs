using UnityEngine;
using System.Collections;

public class SpawningEchos : MonoBehaviour {

    EchoManager em;
    public float intervalTimeOfEchos = 4.0f;
    public float maxRadius = 10.0f;
    public float fade = 0.0f;
    public float speed = 9.0f;
    private float spawnTime = 0.0f;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (em == null)
        {
            em = GameObject.Find("EchoManager").GetComponent<EchoManager>();
        }

        spawnTime += Time.deltaTime;
        if (spawnTime >= intervalTimeOfEchos)
        {
            spawnTime = 0.0f;
            GameObject temp = em.spawnAnEchoLocation(gameObject.transform.position);
            EchoSpawner t = temp.GetComponent<EchoSpawner>();
            t.maxRadius = maxRadius;
            t.fade = fade;
            t.echoSpeed = speed;
        }

	}
}
