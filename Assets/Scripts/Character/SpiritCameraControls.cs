using UnityEngine;
using System.Collections;

public class SpiritCameraControls : MonoBehaviour {
    public float velocity;
    public float cameraDistance;
    public float cameraHeight;
    public float turnSpeed;
    public Transform player;

    private Vector3 offset;

<<<<<<< HEAD
    // Use this for initialization
    void Start()
    {
        offset = new Vector3(0, cameraHeight, cameraDistance);
	
	}
	    
=======
    void Start()
    {
        offset = new Vector3(0, cameraHeight, cameraDistance);
    }

>>>>>>> origin/Week2-Playtest
    void LateUpdate()
    {
        if (player == null)
        {
            GameObject play = GameObject.Find("PhysicalBodyLocal(Clone)");
            if (play != null)
            {
                player = play.transform;
            }
        }

        offset = Quaternion.AngleAxis(Input.GetAxis("RotateCamera") * turnSpeed, Vector3.up) * offset;
        transform.position = player.position + offset;
        transform.rotation = player.rotation * Quaternion.AngleAxis(90.0f, Vector3.right);
    }
}
