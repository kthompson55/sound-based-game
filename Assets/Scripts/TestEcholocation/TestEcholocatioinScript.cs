using UnityEngine;
using System.Collections;

public class TestEcholocatioinScript : MonoBehaviour {

    public GameObject projector;
    public CollisionTest test;
    bool stop = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (test.isHit&&!stop)
        {
            Debug.Log("Was hit");
            projector.SetActive(true);
            projector.transform.position = test.transform.position;
            projector.transform.Translate(0.0f, 0.0f, -5.0f);
            stop = true;
        }
	}
}
