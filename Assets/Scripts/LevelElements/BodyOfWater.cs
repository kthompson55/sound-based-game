using UnityEngine;
using System.Collections;

public class BodyOfWater : MonoBehaviour 
{
    void OnTriggerEnter(Collider col)
    {
        PhysicalBodyLocal physicalBody = col.GetComponent<PhysicalBodyLocal>();
        if (physicalBody)
        {
            physicalBody.StartSwimming();
        }
        PhysicalBodyWithoutNetworking nonNetwork = col.GetComponent<PhysicalBodyWithoutNetworking>();
        if(nonNetwork)
        {
            nonNetwork.StartSwimming();
        }
    }

    void OnTriggerExit(Collider col)
    {
        PhysicalBodyLocal physicalBody = col.GetComponent<PhysicalBodyLocal>();
        if (physicalBody)
        {
            physicalBody.StopSwimming();
        }
        PhysicalBodyWithoutNetworking nonNetwork = col.GetComponent<PhysicalBodyWithoutNetworking>();
        if (nonNetwork)
        {
            nonNetwork.StopSwimming();
        }
    }
}
