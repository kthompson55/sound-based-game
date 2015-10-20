using UnityEngine;
using System;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterController))]
public class Enemy : MonoBehaviour
{
    public GameObject[] pathPoints;
    public float speed = 10;
    public float turnSpeed = 8;
    public int damage = 10;
    public bool patrollingEnabled;
    public bool tryToWaitAtEachNode;
    public float waitDuration = 2f;
    public float reactionInterval = 4f;
    public float acceptableTurnAngleDiff = 5f;

    private CharacterController controller;
    private Vector3 fromPosition;
    private Vector3 targetPosition;
    private float yMovement = 0;
    private float waitStart;
    private int nextNodeIndex;
    private bool isChasing;
    private bool isWaiting;
    public bool isAlerted;
    public bool startAtFirstNode;

    void Start() {
        nextNodeIndex = 0;
        waitStart = 0;
        fromPosition = transform.position;
        targetPosition = transform.position;
        isChasing = false;
        isWaiting = false;

        controller = GetComponent<CharacterController>();
        if (pathPoints.Length > 0) {
            Vector3 starting = pathPoints[nextNodeIndex].transform.position;
            starting.y = transform.position.y;
            if(startAtFirstNode) transform.position = starting;
            ChangeTarget(starting);
        }
    }

    bool isTurning;
    Quaternion rotation;
    void Update() 
    {
        //waiting after reaching chase location

        if (isChasing)
        {
            Vector3 path = targetPosition - fromPosition;
            path.y = 0;
            Vector3 direction = Vector3.Normalize(path) * Time.deltaTime * speed;
            controller.Move(new Vector3(direction.x, yMovement, direction.z));
            if (ReachedOrOverShotTarget(path, targetPosition))
            {
                isChasing = false;
            }
        }
        else if (isWaiting) 
        {
            if ((System.DateTime.Now.Ticks * 10000) - waitStart > (waitDuration / 1000))
            {
                isWaiting = false;
                isAlerted = false;
                //continue going to current node 
                var currNode = ((nextNodeIndex) < 0) ? pathPoints.Length - 1 : nextNodeIndex;
                ChangeTarget(pathPoints[currNode].transform.position);
            }
        } 
        else if (isTurning) {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, rotation, Time.deltaTime * turnSpeed);
            if ( Math.Abs(transform.rotation.y - rotation.y) < acceptableTurnAngleDiff ) {
                isTurning = false;
            }
        }
        //don't patrol if chasing
        else if (patrollingEnabled)
        {
            Vector3 path = targetPosition - fromPosition;
            path.y = 0;
            Vector3 direction = Vector3.Normalize(path) * Time.deltaTime * speed;
            controller.Move(new Vector3(direction.x, yMovement, direction.z));
            if (ReachedOrOverShotTarget(path, targetPosition))
            {
                if (tryToWaitAtEachNode)
                {
                    StartWaiting();
                }
                NextTarget();
            }
        }
        
    }

    void StartWaiting() {
        isWaiting = true;
        waitStart = System.DateTime.Now.Ticks * 10000;
    }

    bool ReachedOrOverShotTarget(Vector3 path, Vector3 target){
        return ( 
            (transform.position.z == target.z && transform.position.x == target.x)
            || Vector3.Magnitude(fromPosition - transform.position) > Vector3.Magnitude(path));
    }
    //auto change chase target to next point in path points []
    void NextTarget() {
        if (pathPoints.Length > 0) {
            nextNodeIndex = (nextNodeIndex >= pathPoints.Length - 1) ? 0 : nextNodeIndex + 1;
            ChangeTarget(pathPoints[nextNodeIndex].transform.position);
        }
    }

    //shift change chase target to specific point
    void ChangeTarget(Vector3 targetPos) {
        fromPosition = transform.position;
        targetPosition = targetPos;


        rotation = Quaternion.LookRotation(CancelY(targetPosition - fromPosition));
        isTurning = true;
    }

    Vector3 CancelY(Vector3 toCancel){
        return new Vector3(toCancel.x, 0, toCancel.z);
    }

    public void ChasePlayer(Collider player) {
        isAlerted = true;
        isChasing = true;
        ChangeTarget(player.transform.position);
    }

    public Vector3 GetTargetPosition() {
        return targetPosition;
    }

    public bool IsAlerted()
    {
        return isAlerted;
    }
}


//enemy patrols around nodes
//if player is within fov range, will update lastSeenPosition
    //TODO fov will continue to check if player is in range depending on enemy's reaction time
        //TODO slower reactions will result in a less constant check
//enemy will lerp towards lastSeenPosition 
    //damage player on collision
    //ensure enemy collider object also has enemy tag
//Once enemy reaches the last seen position
    // if player is no longer in range
        //wait for a moment
        //then go back to patrolling (resume targeting last node target)
    //FOV TODO if player is still in range
        //TODO reset fov reaction timer