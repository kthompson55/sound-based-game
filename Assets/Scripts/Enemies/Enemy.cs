using UnityEngine;
using System;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterController))]
public class Enemy : MonoBehaviour
{
    public GameObject[] pathPoints;
    public float speed;
    public int damage;
    public bool isPatrolling;
    public float waitDuration;

    private CharacterController controller;
    private Vector3 fromPosition;
    private Vector3 targetPosition;
    private Vector3 lastSeenPlayerLocation; //when the player enters the FOV of enemy, this will be updated by enemyFOV script
    private float yMovement = 0;
    private float waitStart;
    private int nextNodeIndex;
    private bool isChasing;
    private bool isWaiting;

    void Start() {
        nextNodeIndex = 0;
        waitStart = 0;
        controller = GetComponent<CharacterController>();
        if (pathPoints.Length > 0)
            transform.position = pathPoints[nextNodeIndex].transform.position;

        fromPosition = transform.position;
        targetPosition = transform.position;
        NextTarget();
        isChasing = false;
        isWaiting = false;
    }

    void Update() 
    {
        //waiting after reaching chase location
        if (isWaiting) 
        {
            Vector3 path = new Vector3();
            if ((System.DateTime.Now.Ticks * 10000) - waitStart > (waitDuration / 1000))
            {
                isWaiting = false;
                ChangeTarget(pathPoints[nextNodeIndex].transform.position);
            }
        } 
        else if (isChasing) 
        {
            Vector3 path = targetPosition - fromPosition;
            Vector3 direction = Vector3.Normalize(path) * Time.deltaTime * speed;
            controller.Move(new Vector3(direction.x, yMovement, direction.z));
            if (ReachedOrOverShotTarget(path, targetPosition))
            {
                isChasing = false;
                isWaiting = true;
                waitStart = System.DateTime.Now.Ticks * 10000;
            }
        }
        //don't patrol if chasing
        else if (isPatrolling)  {
            Vector3 path = targetPosition - fromPosition;
            Vector3 direction = Vector3.Normalize(path) * Time.deltaTime * speed;
            controller.Move(new Vector3(direction.x, yMovement, direction.z));
            if (ReachedOrOverShotTarget(path, targetPosition)){
                NextTarget();
            }
        }
        
    }



    bool ReachedOrOverShotTarget(Vector3 path, Vector3 target){
        return (transform.position == target
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
        gameObject.transform.LookAt(targetPos);
        fromPosition = transform.position;
        targetPosition = targetPos;
    }

    public void ChasePlayer(Collider player) {
        isChasing = true;
        ChangeTarget(player.transform.position);
    }
}


//enemy patrols around nodes
//TODO if player is within fov range, will update lastSeenPosition
    //TODO fov will continue to check if player is in range depending on enemy's reaction time
        //TODO slower reactions will result in a less constant check
//TODO enemy will lerp towards lastSeenPosition 
    //damage player on collision
    //TODO ensure enemy collider object also has enemy tag
//TODO Once enemy reaches the last seen position
    //TODO if player is no longer in range
        //TODO wait for a moment
        //TODO then go back to patrolling (resume targeting last node target)
    //FOV TODO if player is still in range
        //TODO reset fov reaction timer