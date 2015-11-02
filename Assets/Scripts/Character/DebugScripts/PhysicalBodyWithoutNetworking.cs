﻿using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
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
    public float jumpMovementModifier;
    public float hCameraSpeed;
    public float vCameraSpeed;
    public float posCameraBounds;
    public float negCameraBounds;
    public bool isSwimming;
    public float swimMovementRate;
    public float sinkSpeed;
    public float swimUpSpeed;

    private CharacterController controller;
    private Rigidbody rigidbody;
    private bool jumping;
    private float jumpTracking;
    private Vector3 jumpingMovementDirection;
    private float hRotation;
    private float vRotation;
    private bool leavingWater;
    private float currJumpCap = 0;

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
                float jumpShift = jumpSpeed * Time.deltaTime;
                jumpTracking += jumpShift;
                yMovement = jumpShift + Physics.gravity.y * Time.deltaTime;
                if (jumpTracking >= jumpHeight || !Input.GetButton("Jump"))
                {
                    jumping = false;
                }
            }
            else
            {
                yMovement = Physics.gravity.y * Time.deltaTime;
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

        // change rotation based on current camera angle
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, hRotation, transform.localEulerAngles.z);
        Quaternion cameraRotation = Quaternion.Euler(transform.localEulerAngles);
        followingCamera.transform.position = new Vector3(followingCamera.transform.position.x, transform.position.y + vRotation, followingCamera.transform.position.z);
        // apply movement
        Vector3 moveVector = new Vector3(xMovement, yMovement, zMovement);
        controller.Move(cameraRotation * moveVector);
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
