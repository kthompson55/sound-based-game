using UnityEngine;
using System.Collections;

public class EchoManager : MonoBehaviour {

    public GameObject whatToSpawn;

    private bool stop = false;

    private static int MAX_NUMBER_OF_ECHOS = 10;

    private bool[] echoArray = new bool[MAX_NUMBER_OF_ECHOS];

    private int nextAvaibleSpot=0;

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

    public void spotOpen(int whatSpot)
    {
        if (whatSpot < nextAvaibleSpot)
        {
            nextAvaibleSpot = whatSpot;
        }
        echoArray[whatSpot] = false;
    }

    public void spawnAnEchoLocation(Vector3 where)
    {
        (whatToSpawn.GetComponent<EchoSpawner>()).setEchoLocation(nextAvaibleSpot);
        echoArray[nextAvaibleSpot] = true;
        var temp=Instantiate(whatToSpawn, where, new Quaternion());
        ((GameObject)temp).GetComponent<EchoSpawner>().subscribeToDeath(this);
        ((GameObject)temp).GetComponent<EchoSpawner>().setEchoLocation(nextAvaibleSpot);

        getNextSpot();
    }

    private void getNextSpot()
    {
        for (int run = nextAvaibleSpot + 1; run < MAX_NUMBER_OF_ECHOS; run++ )
        {

            if (!echoArray[run])
            {
                nextAvaibleSpot = run;
                break;
            }
        }

    }
}
