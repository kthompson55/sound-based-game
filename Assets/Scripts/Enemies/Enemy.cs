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
    private float yMovement = 0;
    private float waitStart;
    private int nextNodeIndex;
    private bool isChasing;
    private bool isWaiting;
    public bool stopChasing;
    private DateTime waitSoundTime;

    void Start() {
        nextNodeIndex = 0;
        waitStart = 0;
        waitSoundTime = System.DateTime.Now;
        controller = GetComponent<CharacterController>();
        if (pathPoints.Length > 0) {
            Vector3 starting = pathPoints[nextNodeIndex].transform.position;
            starting.y = transform.position.y;
            transform.position = starting;
            NextTarget();
        }
            

        fromPosition = transform.position;
        targetPosition = transform.position;
        isChasing = false;
        isWaiting = false;
    }

    void Update() 
    {
        //waiting after reaching chase location
        if (isWaiting) 
        {
            if ((System.DateTime.Now.Ticks * 10000) - waitStart > (waitDuration / 1000))
            {
                isWaiting = false;
                if(pathPoints.Length > 0)
                    ChangeTarget(pathPoints[nextNodeIndex].transform.position);
            }
        } 
        else if (isChasing) 
        {
            Vector3 path = targetPosition - fromPosition;
            path.y = 0;
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
            path.y = 0;
            Vector3 direction = Vector3.Normalize(path) * Time.deltaTime * speed;
            controller.Move(new Vector3(direction.x, yMovement, direction.z));
            if (ReachedOrOverShotTarget(path, targetPosition)){
                NextTarget();
            }
        }
        
        if(System.DateTime.Now.Subtract(waitSoundTime).Seconds>=10){
            Debug.Log("Spawn");
            waitSoundTime = System.DateTime.Now;
            EchoManager things = GameObject.Find("EchoManager").GetComponent<EchoManager>();
            GameObject.Find("EchoManager").GetComponent<EchoManager>().spawnAnEchoLocation(gameObject.transform.position);
        }

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
        gameObject.transform.LookAt(new Vector3(targetPos.x, gameObject.transform.position.y, targetPos.z));
        fromPosition = transform.position;
        targetPosition = targetPos;
    }

    public void ChasePlayer(Collider player) {
            isChasing = true;
            ChangeTarget(player.transform.position);
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