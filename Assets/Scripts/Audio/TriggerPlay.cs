using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSpawn))]
[RequireComponent(typeof(BoxCollider))]
public class TriggerPlay : MonoBehaviour
{
    EchoManager em;
    public float SoundDuration = 10.0f;
    public float maxRadius = 10.0f;
    public float fade = 0.0f;
    public float speed = 9.0f;
    public int numEchos = 1;
    public float timeBetweenEchos = 1.0f;
    private float spawnTime = 0.0f;
    public Color echoColor = Color.white;
    private bool spawnEchos = false;
    private int numEchoSpawned = 0;
    private float timeSinceLastEcho = float.MaxValue;
    // Use this for initialization
    void Start()
    {

    }

    void OnTriggerEnter(Collider collide)
    {
        if (collide.GetComponent<PhysicalBodyWithoutNetworking>() != null
            || collide.GetComponent<PhysicalBodyLocal>() != null)
        {
            GetComponent<AudioSpawn>().spawn(SoundDuration);
            spawnEchos = true;
            timeSinceLastEcho = float.MaxValue;
            numEchoSpawned = 0;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (em == null)
        {
            em = GameObject.Find("EchoManager").GetComponent<EchoManager>();
        }

        if(spawnEchos){
            timeSinceLastEcho += Time.deltaTime;
            if (timeSinceLastEcho > timeBetweenEchos)
            {
                spawnTime = 0.0f;
                GameObject temp = em.spawnAnEchoLocation(gameObject.transform.position);
                EchoSpawner t = temp.GetComponent<EchoSpawner>();
                //Debug.Log(gameObject.name + ": "+ t.getEchoLocation());
                t.echoColor = echoColor;
                t.maxRadius = maxRadius;
                t.fade = fade;
                t.echoSpeed = speed;

                timeSinceLastEcho = 0.0f;
                ++numEchoSpawned;
                if (numEchoSpawned > numEchos)
                {
                    timeSinceLastEcho = float.MaxValue;
                    numEchoSpawned = 0;
                    spawnEchos = false;
                }
            }
        }
    }
}