using UnityEngine;
using System.Collections;

public class BodyOfWater : MonoBehaviour 
{
    void OnTriggerEnter(Collider col)
    {
        PhysicalBodyLocal physicalBody = col.GetComponent<PhysicalBodyLocal>();
        if (physicalBody)
        {
            physicalBody.isSwimming = true;
        }
        PhysicalBodyWithoutNetworking nonNetwork = col.GetComponent<PhysicalBodyWithoutNetworking>();
        if(nonNetwork)
        {
            nonNetwork.isSwimming = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        PhysicalBodyLocal physicalBody = col.GetComponent<PhysicalBodyLocal>();
        if (physicalBody)
        {
            physicalBody.isSwimming = false;
        }
        PhysicalBodyWithoutNetworking nonNetwork = col.GetComponent<PhysicalBodyWithoutNetworking>();
        if (nonNetwork)
        {
            nonNetwork.isSwimming = false;
        }
    }
}
