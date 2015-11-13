using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using UnityStandardAssets.ImageEffects;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterController))]
public class PhysicalBodyLocal : NetworkBehaviour
{
    public GameObject followingCamera;
    public GameObject otherCamera;
    public float speed;
    public float jumpHeight;
    public float jumpSpeed;
    public float jumpMovementModifier;
    public float gravityEffectModifier;
    public float hCameraSpeed;
    public float vCameraSpeed;
    public float posCameraBounds;
    public float negCameraBounds;
    public bool isSwimming;
    public float swimMovementRate;
    public float sinkSpeed;
    public float swimUpSpeed;

    private EchoManager em;
    private DateTime waitSoundTime;

    private CharacterController controller;
    private Rigidbody rigidbody;
    private bool jumping;
    private float jumpTracking;
    private Vector3 jumpingMovementDirection;
    private float hRotation;
    private float vRotation;
    private bool leavingWater;
    private float currJumpCap = 0;
    private float gravityVelocity;

    void Start()
    {
        waitSoundTime = System.DateTime.Now;
        controller = GetComponent<CharacterController>();
        rigidbody = GetComponent<Rigidbody>();
        jumping = false;
        currJumpCap = gameObject.transform.localPosition.y + jumpHeight;
    }


    private bool jumpLocked = false;
    void Update()
    {
        if (em == null)
        {
            em = GameObject.Find("EchoManager").GetComponent<EchoManager>();
            waitSoundTime = System.DateTime.Now;
            GameObject temp = em.spawnAnEchoLocation(gameObject.transform.position);
            temp.GetComponent<EchoSpawner>().echoSpeed = 1f;
            temp.GetComponent<EchoSpawner>().maxRadius = 2;
        }

        if (System.DateTime.Now.Subtract(waitSoundTime).Seconds >= 1)
        {
            waitSoundTime = System.DateTime.Now;
            GameObject temp = em.spawnAnEchoLocation(gameObject.transform.position);
            temp.GetComponent<EchoSpawner>().echoSpeed = 1f;
            temp.GetComponent<EchoSpawner>().maxRadius = 2;

        }

        //Don't move other people plz.
        if (!isLocalPlayer) return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("R presssed");
            RpcLoadLevel("RPC called");
        }

        #region Camera Fixes
        if (followingCamera == null)
        {
            GameObject newCam = GameObject.Find("PhysicalCamera");
            if (newCam != null)
            {
                followingCamera = newCam;
                followingCamera.transform.parent = transform;
            }
        }
        if (otherCamera == null)
        {
            GameObject newCam = GameObject.Find("SpiritualCamera");
            if (newCam != null)
            {
                otherCamera = newCam;
                otherCamera.transform.parent = transform;
            }
        }

        followingCamera.SetActive(true);
        otherCamera.SetActive(false);
        #endregion

        float yMovement = 0;
        float xMovement = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float zMovement = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        Vector3 currentMovementNormal = new Vector3(xMovement, 0, zMovement).normalized;

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
        else if (leavingWater && Input.GetButton("Jump"))
        {
            if (controller.isGrounded)
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
            if (controller.isGrounded && !jumping)
            {
                if (Input.GetButton("Jump"))
                {
                    jumpTracking = 0;
                    jumping = true;
                    jumpingMovementDirection = new Vector3(xMovement, 0, zMovement);
                    currJumpCap = transform.localPosition.y + jumpHeight + Physics.gravity.y * Time.deltaTime;
                }
            }
            else if (jumping)
            {
                gravityVelocity = (currJumpCap - transform.localPosition.y) / gravityEffectModifier;
                float jumpShift = jumpSpeed * Time.deltaTime * gravityVelocity;
                jumpTracking += jumpShift;
                yMovement = jumpShift + Physics.gravity.y * Time.deltaTime;
                if (jumpTracking >= jumpHeight || !Input.GetButton("Jump"))
                {
                    jumping = false;
                }
            }
            else
            {
                gravityVelocity += Physics.gravity.y * Time.deltaTime;
                yMovement = gravityVelocity * Time.deltaTime;
            }
        }
    }

    [ClientRpc]
    void RpcLoadLevel(string nextLevel)
    {
        Debug.Log(nextLevel);
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