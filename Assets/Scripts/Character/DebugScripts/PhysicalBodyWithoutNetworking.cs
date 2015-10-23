using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterController))]
public class PhysicalBodyWithoutNetworking : MonoBehaviour
{
    public GameObject followingCamera;
    public GameObject otherCamera;
    public float speed;
    public float jumpHeight;
    public float jumpSpeed;
    public float tapJumpTime;
    public float jumpMovementModifier;

    private CharacterController controller;
    private Rigidbody rigidbody;
    private bool jumping;
    private float jumpTracking;
    private Vector3 jumpingMovementDirection;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        rigidbody = GetComponent<Rigidbody>();
        jumping = false;
    }

    void Update()
    {
        #region Camera Fixes
        if (followingCamera == null)
        {
            GameObject newCam = GameObject.Find("PhysicalCamera");
            if (newCam != null)
            {
                followingCamera = newCam;
            }
        }
        if (otherCamera == null)
        {
            //Debug.Log("No other physical");
            GameObject newCam = GameObject.Find("SpiritualCamera");
            if (newCam != null)
            {
                otherCamera = newCam;
            }
        }

        followingCamera.SetActive(true);
        otherCamera.SetActive(false);
        #endregion

        float xMovement = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float zMovement = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        Vector3 currentMovementNormal = new Vector3(xMovement, 0, zMovement).normalized;
        // reduce movement speed if attempting to move in a direction different from your running direction prior to jumping
        if (!controller.isGrounded)
        {
            Vector3 directionNormal = jumpingMovementDirection.normalized;
            float totalDifferenceFromDirection = Mathf.Abs(currentMovementNormal.x - directionNormal.x) + Mathf.Abs(currentMovementNormal.z - directionNormal.z);
            if (totalDifferenceFromDirection > .3f)
            {
                xMovement *= jumpMovementModifier;
                zMovement *= jumpMovementModifier;
            }
        }

        // handle upward and downward movements of jumping
        float yMovement = 0;
        if (controller.isGrounded && !jumping)
        {
            if (Input.GetButton("Jump"))
            {
                Vector3 jumpForce = new Vector3(0, jumpHeight, 0);
                jumpTracking = 0;
                jumping = true;
                jumpingMovementDirection = new Vector3(xMovement, 0, zMovement);
                StartCoroutine("VerifyFullJump");
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

        // change rotation based on current camera angle
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, followingCamera.transform.eulerAngles.y, transform.localEulerAngles.z);
        Quaternion cameraRotation = Quaternion.Euler(transform.localEulerAngles);
        // apply movement
        Vector3 moveVector = new Vector3(xMovement, yMovement, zMovement);
        controller.Move(cameraRotation * moveVector);
    }

    // Is the jump button being tapped or held?
    IEnumerator VerifyFullJump()
    {
        yield return new WaitForSeconds(tapJumpTime);
        jumping = Input.GetButton("Jump");
    }
}
