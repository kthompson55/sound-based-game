using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterController))]
public class PhysicalBodyLocal : NetworkBehaviour
{
    public GameObject followingCamera;
    public GameObject otherCamera;
    public float speed;
    public float jumpHeight;
    public float jumpSpeed;

    private CharacterController controller;
    private Rigidbody rigidbody;
    private bool jumping;
    private float jumpTracking;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        rigidbody = GetComponent<Rigidbody>();
        jumping = false;
    }

    void Update()
    {
        //Don't move other people plz.
        if (!isLocalPlayer) return;

        #region Camera Fixes
        if (followingCamera == null)
        {
            Debug.Log("No following physical");
            GameObject newCam = GameObject.Find("PhysicalCamera");
            if (newCam != null)
            {
                Debug.Log("Found following physical");
                followingCamera = newCam;
            }
        }
        if (otherCamera == null)
        {
            Debug.Log("No other physical");
            GameObject newCam = GameObject.Find("SpiritualCamera");
            if (newCam != null)
            {
                Debug.Log("Found other physical");
                otherCamera = newCam;
            }
        }

        followingCamera.SetActive(true);
        otherCamera.SetActive(false);
#endregion

        float yMovement = 0;
        if (controller.isGrounded && !jumping)
        {
            if (Input.GetButton("Jump"))
            {
                Vector3 jumpForce = new Vector3(0, jumpHeight, 0);
                jumpTracking = 0;
                jumping = true;
            }
        }
        else if (jumping)
        {
            float jumpShift = jumpSpeed * Time.deltaTime;
            jumpTracking += jumpShift;
            yMovement = jumpShift;
            if (jumpTracking >= jumpHeight)
            {
                jumping = false;
            }
        }
        else
        {
            yMovement = Physics.gravity.y * Time.deltaTime;
        }
        float xMovement = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float zMovement = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, followingCamera.transform.localEulerAngles.y, transform.localEulerAngles.z);
        Quaternion cameraRotation = Quaternion.Euler(transform.localEulerAngles);
        Vector3 moveVector = new Vector3(xMovement, yMovement, zMovement);
        controller.Move(cameraRotation * moveVector);
    }
}
