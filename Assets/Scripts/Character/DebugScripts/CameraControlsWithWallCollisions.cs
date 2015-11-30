//(Created CSharp Version) 10/2010: Daniel P. Rossi (DR9885) 

using UnityEngine;
using System.Collections;

public class CameraControlsWithWallCollisions : MonoBehaviour
{
    public Transform target = null;
    public float distance;
    public float height;
    public float damping;
    public bool smoothRotation;
    public float rotationDamping;
    public Vector3 targetLookAtOffset; // allows offsetting of camera lookAt, very useful for low bumper heights

    public float bumperDistanceCheck; // length of bumper ray
    public float bumperCameraHeight; // adjust camera height while bumping
    public Vector3 bumperRayOffset; // allows offset of the bumper ray from target origin

    /// <Summary>
    /// If the target moves, the camera should child the target to allow for smoother movement. DR
    /// </Summary>
    private void Awake()
    {
        transform.parent = target;
    }

    private void FixedUpdate()
    {
        if(target)
        {
            //Vector3 wantedPosition = target.TransformPoint(0, height, -distance);
            Vector3 wantedPosition = target.TransformPoint(0, 0, -distance);
            wantedPosition = new Vector3(wantedPosition.x, transform.position.y, wantedPosition.z);

            // check to see if there is anything behind the target
            RaycastHit hit;
            Vector3 back = target.transform.TransformDirection(-1 * Vector3.forward);

            // cast the bumper ray out from rear and check to see if there is anything behind
            if (Physics.Raycast(target.TransformPoint(bumperRayOffset), back, out hit, bumperDistanceCheck)
                && hit.transform != target) // ignore ray-casts that hit the user. DR
            {
                // clamp wanted position to hit position
                wantedPosition.x = hit.point.x;
                wantedPosition.z = hit.point.z;
                wantedPosition.y = Mathf.Lerp(hit.point.y + bumperCameraHeight, wantedPosition.y, Time.deltaTime * damping);
            }
            transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * damping);

            Vector3 lookPosition = target.TransformPoint(targetLookAtOffset);

            if (smoothRotation)
            {
                Quaternion wantedRotation = Quaternion.LookRotation(lookPosition - transform.position, target.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);
            }
            else
            {
                transform.rotation = Quaternion.LookRotation(lookPosition - transform.position, target.up);
            }
        }
    }

    void LateUpdate()
    {
        if(!target)
        {
            GameObject play = GameObject.Find("PhysicalBody_working(Clone)");
            if (play != null)
            {
                target = play.transform;
                transform.parent = target;
            }
        }
        transform.LookAt(target.position);
    }
}