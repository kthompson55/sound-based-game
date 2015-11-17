using UnityEngine;
using System.Collections;

public class EchoManager : MonoBehaviour {

    public GameObject whatToSpawn;

    private bool stop = false;

    private static int MAX_NUMBER_OF_ECHOS = 20;

    private bool[] echoArray = new bool[MAX_NUMBER_OF_ECHOS];

    private int nextAvaibleSpot=0;

    public void spotOpen(int whatSpot)
    {
        if (whatSpot < nextAvaibleSpot)
        {
            nextAvaibleSpot = whatSpot;
        }
        echoArray[whatSpot] = false;
    }

    //Spawns defualt white echos
    public GameObject spawnAnEchoLocation(Vector3 where)
    {
        (whatToSpawn.GetComponent<EchoSpawner>()).setEchoLocation(nextAvaibleSpot);
        echoArray[nextAvaibleSpot] = true;
        Object temp = Instantiate(whatToSpawn, where, new Quaternion());
        ((GameObject)temp).GetComponent<EchoSpawner>().subscribeToDeath(this);
        ((GameObject)temp).GetComponent<EchoSpawner>().setEchoLocation(nextAvaibleSpot);

        getNextSpot();
        return (GameObject)temp;
    }

    //Spawns the echo of the passed in color
    public GameObject spawnAnEchoLocation(Color c,Vector3 where)
    {
        (whatToSpawn.GetComponent<EchoSpawner>()).setEchoLocation(nextAvaibleSpot);
        whatToSpawn.GetComponent<EchoSpawner>().echoMaterial.SetColor("_MainColor" + nextAvaibleSpot, c);
        echoArray[nextAvaibleSpot] = true;
        Object temp = Instantiate(whatToSpawn, where, new Quaternion());
        ((GameObject)temp).GetComponent<EchoSpawner>().subscribeToDeath(this);
        ((GameObject)temp).GetComponent<EchoSpawner>().setEchoLocation(nextAvaibleSpot);
        ((GameObject)temp).GetComponent<EchoSpawner>().echoColor = c;
        (whatToSpawn.GetComponent<EchoSpawner>()).echoColor = Color.white;
        ((GameObject)temp).GetComponent<EchoSpawner>().Starts = true;

        getNextSpot();
        return (GameObject)temp;
    }

    private void testCode(Vector3 where)
    {
        Ray r = new Ray(where, Vector3.down);
        RaycastHit hit;
        if(Physics.Raycast(r,out hit,Mathf.Infinity)){
            spawnAnEchoLocation(hit.point);
        }
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
