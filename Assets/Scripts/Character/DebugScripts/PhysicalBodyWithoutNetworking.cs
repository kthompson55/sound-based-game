using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PhysicalBodyWithoutNetworking : MonoBehaviour
{
    public GameObject followingCamera;
    public GameObject otherCamera;

    public float speed;
    public float runMultiplier;
    public float jumpHeight;
    public float jumpSpeed;
    public float jumpMovementModifier;
    public float jumpingGravityEffectModifier;
    public float hCameraSpeed;
    public float vCameraSpeed;
    public float posCameraBounds;
    public float negCameraBounds;
    public bool isSwimming;
    public float swimMovementRate;
    public float sinkSpeed;
    public float swimUpSpeed;

    private Animator animator;
    private CharacterController controller;
    private Rigidbody rigidbody;
    private bool jumping;
    private float jumpTracking;
    private Vector3 jumpingMovementDirection;
    private float hRotation;
    private float previousHRotation;
    private float vRotation;
    private bool leavingWater;
    private float currJumpCap = 0;
    private float gravityVelocity;
    private float fallTime;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        jumping = false;
        gravityVelocity = 0;
        previousHRotation = 0;
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

        float yMovement = 0;
        float xMovement = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float zMovement = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        Vector3 currentMovementNormal = new Vector3(xMovement, 0, zMovement).normalized;
        bool sprinting = false;

        if (isSwimming)
        {
            xMovement *= swimMovementRate;
            zMovement *= swimMovementRate;
            if(Input.GetButton("Jump"))
            {
                yMovement = swimUpSpeed * Time.deltaTime;
            }
            else
            {
                yMovement = -sinkSpeed * Time.deltaTime;
            }
        }
        else if(leavingWater && Input.GetButton("Jump"))
        {
            if(controller.isGrounded)
            {
                leavingWater = false;
            }
            else
            {
                yMovement = 0;
            }
        }
        else
        {
            if (controller.isGrounded && !jumping)
            {
                gravityVelocity += Physics.gravity.y * Time.deltaTime;
                yMovement = gravityVelocity * Time.deltaTime;
                fallTime = 0;

                if (Input.GetButton("Jump"))
                {
                    jumpTracking = 0;
                    jumping = true;
                    jumpingMovementDirection = new Vector3(xMovement, 0, zMovement);
		            currJumpCap = transform.localPosition.y + jumpHeight + Physics.gravity.y * Time.deltaTime;
                }
                else if(Input.GetButton("Sprint"))
                {
                    sprinting = true;
                    xMovement *= runMultiplier;
                    zMovement *= runMultiplier;
                }
            }
            else if(!controller.isGrounded)
            {
                if (!jumping)
                {
                    gravityVelocity = (Physics.gravity.y * Time.deltaTime * fallTime);
                    yMovement = gravityVelocity;
                    fallTime += Time.deltaTime * 3.0f;
                }
                else
                {
                    gravityVelocity = (currJumpCap - transform.localPosition.y) / jumpingGravityEffectModifier;
                    float jumpShift = (jumpSpeed + gravityVelocity) * Time.deltaTime;
                    jumpTracking += jumpShift;
                    yMovement = jumpShift;
                    if (jumpTracking >= jumpHeight)
                    {
                        jumping = false;
                        fallTime = 0;
                    }
                }
            }
        }

        // adjust rotationMovement
        hRotation += Input.GetAxisRaw("RotateCameraHorizontal") * Time.deltaTime * hCameraSpeed;
        vRotation += Input.GetAxisRaw("RotateCameraVertical") * Time.deltaTime * vCameraSpeed;
        if (vRotation > posCameraBounds)
        {
            vRotation = posCameraBounds;
        }
        else if (vRotation < negCameraBounds)
        {
            vRotation = negCameraBounds;
        }

        Vector2 groundMotion = new Vector2(xMovement, zMovement);
        animator.SetBool("OnGround", controller.isGrounded);
        if(groundMotion.magnitude > 0)
        {
            if(sprinting)
            {
                animator.SetFloat("Forward", 1.0f);
            }
            else
            {
                animator.SetFloat("Forward", .7f);
            }
        }
        else
        {
            animator.SetFloat("Forward", 0);
        }

        if (previousHRotation > hRotation)
        {
            animator.SetFloat("Turn", -100);
        }
        else if (previousHRotation < hRotation)
        {
            animator.SetFloat("Turn", 100);
        }
        else
        {
            animator.SetFloat("Turn", 0);
        }
        
        // change rotation based on current camera angle
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, hRotation, transform.localEulerAngles.z);
        Quaternion cameraRotation = Quaternion.Euler(transform.localEulerAngles);
        followingCamera.transform.position = new Vector3(followingCamera.transform.position.x, transform.position.y + vRotation, followingCamera.transform.position.z);
        // apply movement
        Vector3 moveVector = new Vector3(xMovement, yMovement, zMovement);
        controller.Move(cameraRotation * moveVector);

        previousHRotation = hRotation;
    }

    public void StartSwimming()
    {
        isSwimming = true;
        followingCamera.GetComponent<Blur>().enabled = true;
    }

    public void StopSwimming()
    {
        isSwimming = false;
        leavingWater = true;
        followingCamera.GetComponent<Blur>().enabled = false;
    }
}
