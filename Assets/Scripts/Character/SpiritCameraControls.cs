using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SpiritCameraControls : NetworkBehaviour
{
    public float velocity;
    public float cameraDistance;
    public float cameraHeight;
    public float turnSpeed;
    public Transform player;

    private Vector3 offset;

    void Start()
    {
        //player = GameObject.Find("");
        offset = new Vector3(0, cameraHeight, cameraDistance);
    }

    void LateUpdate()
    {
        if (player == null)
        {
            GameObject play = GameObject.Find("PhysicalBody_working(Clone)");
            if (play != null)
            {
                player = play.transform;
            }
        }

        offset = Quaternion.AngleAxis(Input.GetAxis("RotateCameraHorizontal") * turnSpeed, Vector3.up) * offset;
        transform.position = player.position + offset;
        transform.rotation = player.rotation * Quaternion.AngleAxis(90.0f, Vector3.right);
    }
}
