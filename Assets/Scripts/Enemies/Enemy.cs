using UnityEngine;
using System;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterController))]
public class Enemy : MonoBehaviour
{
	public int damage;
    public GameObject[] pathPoints;
    private Vector3 fromPosition;
    private Vector3 toPosition;
    private CharacterController controller;
    public bool moving;
    public float speed;
    private int nextPointIndex;

    void Start() {
        nextPointIndex = 0;
        controller = GetComponent<CharacterController>();
        if (pathPoints.Length > 0)
            transform.position = pathPoints[nextPointIndex].transform.position;

        fromPosition = transform.position;
        toPosition = transform.position;
        NextTarget();
    }

    private float yMovement = 0;
    void Update() {
        //if enemies have the ability to get swept up, don't move them in the air
        if (moving) {

            Vector3 path = toPosition - fromPosition;
            Vector3 direction = Vector3.Normalize(path);
            direction *= Time.deltaTime * speed;
            float xMovement = direction.x * Time.deltaTime * speed;
            float zMovement = direction.z * Time.deltaTime * speed;
            
            Vector3 moveVector = new Vector3(xMovement, yMovement, zMovement);
            //transform.position += direction;// moveVector;
            Debug.Log(moveVector);
            controller.Move(moveVector);
            //either at target or overshot target
            if (transform.position == toPosition 
                || Vector3.Magnitude(fromPosition - transform.position) > Vector3.Magnitude(path))
            {
                NextTarget();
            }
        }
    }


    
    //auto change chase target to next point in path points []
    void NextTarget() {
        if (pathPoints.Length > 0) {
            nextPointIndex = (nextPointIndex > pathPoints.Length - 1) ? 0 : nextPointIndex + 1;
            ChangeTarget(pathPoints[nextPointIndex].transform.position);
        }
    }

    //shift change chase target to specific point
    void ChangeTarget(Vector3 targetPos) {
        gameObject.transform.LookAt(targetPos);
        fromPosition = transform.position;
        toPosition = targetPos;
    }
}


