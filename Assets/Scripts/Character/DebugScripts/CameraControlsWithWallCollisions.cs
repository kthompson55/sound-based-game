using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class CameraControlsWithWallCollisions : MonoBehaviour 
{
    public Transform player;
    public float velocity;
    public float cameraDistance;
    public float cameraHeight;
    public float cameraMaxDistance;
    public float cameraSpeed;
    public float cameraFocusHeight;

    private Vector3 targetCameraPosition;
    private Vector3 previousPosition;
    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

	// Update is called once per frame
	void Update () 
    {
        targetCameraPosition += player.position - previousPosition;
        Vector3 vectorDirection = (targetCameraPosition - player.position).normalized;
        vectorDirection.y = 0;
        vectorDirection = vectorDirection.normalized;
        float lastCameraDistance = (transform.position - player.position).magnitude;

        if (Input.GetAxis("RotateCamera") > 0)
        {
            targetCameraPosition += Vector3.Cross(vectorDirection, Vector3.up) * velocity;
            transform.position += Vector3.Cross(vectorDirection, Vector3.up) * velocity;
        }
        if (Input.GetAxis("RotateCamera") < 0)
        {
            targetCameraPosition += Vector3.Cross(Vector3.up, vectorDirection) * velocity;
            transform.position += Vector3.Cross(Vector3.up, vectorDirection) * velocity;
        }

        transform.position = ((transform.position - player.position).normalized * lastCameraDistance) + player.position;
        vectorDirection = (targetCameraPosition - player.position).normalized;
        vectorDirection.y = 0;
        vectorDirection = vectorDirection.normalized;
        targetCameraPosition = (vectorDirection * cameraDistance) + new Vector3(0, cameraHeight, 0) + player.position;
        if ((transform.position - player.position).magnitude > cameraMaxDistance) transform.position = targetCameraPosition;
        else rigidbody.AddForce((targetCameraPosition - player.position) * cameraSpeed);
        transform.LookAt(player.position + new Vector3(0, cameraFocusHeight, 0));
	}
}
