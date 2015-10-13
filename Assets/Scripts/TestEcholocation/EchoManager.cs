using UnityEngine;
using System.Collections;

public class EchoManager : MonoBehaviour {

    public GameObject whatToSpawn;

    private bool stop = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0)&&!stop)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                spawnAnEchoLocation(hit.point);
            }
            stop = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            stop = false;
        }

	}

    public void spawnAnEchoLocation(Vector3 where)
    {
        Instantiate(whatToSpawn, where, new Quaternion());
    }
}
