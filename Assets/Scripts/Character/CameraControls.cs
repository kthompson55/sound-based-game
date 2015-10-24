using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraControls : MonoBehaviour 
{
    public float velocity;
    public float cameraDistance;
    public float cameraHeight;
    public float turnSpeed;
    public Transform player;

    private Vector3 offset;

    void Start() 
    {
        offset = new Vector3(0, cameraHeight, cameraDistance);
    }

    void Update()
    {
        transform.rotation = Quaternion.identity;
    }

    void LateUpdate()
    {
        if (player == null)
        {
            GameObject play = GameObject.Find("PhysicalBody_working(Clone)");
            if (play != null)
            {
                player = play.transform;
                transform.parent = player;
            }
        }
    }
}
