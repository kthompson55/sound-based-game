using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SpiritualBody : NetworkBehaviour
{   
    public float attackRange;
    public float speed;
    public GameObject followingCamera;
    public GameObject otherCamera;

    private CharacterController controller;
    private Rigidbody rigidbody;
    private Vector3 position;
    public PhysicalBodyLocal physicalBody;
    public bool attacking;

    void Start()
    {
        attacking = false;
    }
        
    void Update()
    {
        #region Camera Fixes
        if (!isLocalPlayer) return;

        if (followingCamera == null)
        {
            GameObject newCam = GameObject.Find("SpiritualCamera");
            if (newCam != null)
            {
                followingCamera = newCam;
            }
        }
        if (otherCamera == null)
        {
            GameObject newCam = GameObject.Find("PhysicalCamera");
            if (newCam != null)
            {
                otherCamera = newCam;
            }
        }

        if (physicalBody == null)
        {
            GameObject newBod = GameObject.Find("PhysicalBody_working(Clone)");
            if (newBod != null)
            {
                physicalBody = newBod.GetComponent<PhysicalBodyLocal>();
            }
        }

        followingCamera.SetActive(true);
        otherCamera.SetActive(false);
        #endregion

        UpdateIsAttacking();
        UpdateAttack();
    }

    void UpdateIsAttacking()
    {
        if (!attacking&&!returning&&Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                float angle = 0.0f;
                float y = 0.0f;
                float x = 0.0f;
                y = hit.point.z - transform.position.z;
                x = hit.point.x - transform.position.x;
                angle = Mathf.Atan(y / x);
                if (x < 0)
                {
                    angle += (Mathf.PI / 2);
                    angle += (Mathf.PI / 2);
                }

                Attack(angle);
            }
        }
    }

    float attackAngle = 90;
    bool active = false;
    Vector3 attackStart;
    Vector3 attackTarget;
    bool returning = false;
    void UpdateAttack() {

        //attack input has been handled and should now be lerping
        if (attacking)
        {
            //given an attack angle find the x,y pos away from the center point
            //the attack is active if it has been initiated and is not yet retreating
            if (active)
            {
                if (lerpToPosition(attackTarget))
                {
                    active = false;
                }
            }
            else
            {
                attacking = false;
            }
        }
        //if not at body return to body
        else if (transform.position != physicalBody.transform.position)
        {
            ReturnToBody();
        }
        //if at body, enable attack input
        else {
            attacking = false;
        }

    }

    void ReturnToBody() {
        returning=!lerpToPosition(physicalBody.transform.position);
    }

    public void Attack(float angle) {
        if(!attacking){
            attackStart = transform.position;
            Vector2 displacementVector = GetDisplacementVector(angle);
            attackTarget = attackStart + new Vector3(displacementVector.x, 0, displacementVector.y);
            attacking = active = true;
        }
    }

    private Vector2 GetDisplacementVector(float angle) {
        //Cos(theta) = A/H
        float x = Mathf.Cos(angle) * attackRange;
        //Sin(theta) = O/H
        float z = Mathf.Sin(angle) * attackRange;
        return new Vector2(x, z);
    }

    private bool lerpToPosition(Vector3 target) {
        bool reachedTarget = false;
        Vector3 positionThisFrame = transform.position;
        Vector3 path = target - positionThisFrame;
        Vector3 direction = Vector3.Normalize(path);
        Vector3 endMovePos = positionThisFrame + direction * speed * Time.deltaTime;
        if (endMovePos == target || Vector3.Magnitude(endMovePos - positionThisFrame) > Vector3.Magnitude(path)) { 
            //overshot target
            endMovePos = target;
            reachedTarget = true;
        }
        transform.position = endMovePos;
        return reachedTarget;
    }

}
