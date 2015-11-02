using UnityEngine;
using System.Collections;

public class SpiritCompass : MonoBehaviour {

    public GameObject endLocation;

	// Use this for initialization
	void Start () {
        endLocation=GameObject.FindObjectOfType<EndGoal>().gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (endLocation != null)
        {
            gameObject.transform.LookAt(new Vector3(endLocation.transform.position.x, gameObject.transform.position.y, endLocation.transform.position.z));
            gameObject.transform.Rotate(Vector3.forward, 270.0f);
        }
	}
}
