using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SpiritualBody : NetworkBehaviour
{
    public float attackRange;
    public float speed;
    public GameObject followingCamera;
    public GameObject otherCamera;
    public GameObject EchoManage;

    private CharacterController controller;
    private Rigidbody rigidbody;
    private Vector3 position;
    public PhysicalBodyLocal physicalBody;
    public bool attacking;

    void Start()
    {
        GetComponent<BoxCollider>().isTrigger = false;
        GetComponent<MeshRenderer>().enabled = false;
        attacking = false;
    }
        
    void Update()
    {
        #region Camera Fixes
        if (!isLocalPlayer) return;

        if (EchoManage == null)
        {
            GameObject echo = GameObject.Find("EchoManager");
            if (echo != null)
            {
                EchoManage = echo;
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

        if (followingCamera == null)
        {
            GameObject newSpiritCam = GameObject.Find("SpiritualCamera");
            if (newSpiritCam != null)
            {
                followingCamera = newSpiritCam;
                followingCamera.transform.parent = physicalBody.transform;
            }
        }
        if (otherCamera == null)
        {
            GameObject newCam = GameObject.Find("PhysicalCamera");
            if (newCam != null)
            {
                otherCamera = newCam;
                otherCamera.transform.parent = physicalBody.transform;
            }
        }

        followingCamera.SetActive(true);
        otherCamera.SetActive(false);
        #endregion


        //Debug.Log("TransformPosition: " + transform.position);
        //Debug.Log("Physical Position: " + physicalBody.transform.position);

        //Debug.Log("TransformPosition: " + transform.position);

        UpdateIsAttacking();
        UpdateAttack();
    }

    void UpdateIsAttacking()
    {
        //Debug.Log("Is Attacking");
        if (Input.GetMouseButtonDown(1))
        {
            //Debug.Log("Right click");
            Ray ray = ((Camera)followingCamera.GetComponent<Camera>()).ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                EchoManage.GetComponent<EchoManager>().spawnAnEchoLocation(hit.point);
            }
        }

        if (!attacking&&!returning&&Input.GetMouseButtonDown(0))
        {
            Debug.Log("Attacking");
            Ray ray = ((Camera)followingCamera.GetComponent<Camera>()).ScreenPointToRay(Input.mousePosition);
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

        if (returning)
        {
            //Debug.Log("Returning");
            GetComponent<MeshRenderer>().enabled = true;
            if ((position - physicalBody.transform.position).magnitude < 1)
            {
                position = physicalBody.transform.position;
                returning = false;
            }
        }
    }

    float attackAngle = 90;
    bool active = false;
    Vector3 attackStart;
    Vector3 attackTarget;
    bool returning = false;
    void UpdateAttack() {

        //Debug.Log("TransformPosition: " + transform.position);
        //Debug.Log("Physical Position: " + physicalBody.transform.position);

        //attack input has been handled and should now be lerping
        if (attacking)
        {
            //given an attack angle find the x,y pos away from the center point
            //the attack is active if it has been initiated and is not yet retreating
            if (active)
            {
                if (lerpToPosition(attackTarget, speed))
                {
                    active = false;
                }
            }
            else
            {
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<BoxCollider>().enabled = false;
                GetComponent<BoxCollider>().isTrigger = false;
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
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<BoxCollider>().isTrigger = false;
            GetComponent<BoxCollider>().enabled = false;
            attacking = false;
        }

    }

    void ReturnToBody() {
        //transform.position = physicalBody.transform.position;
        //Debug.Log("Returning to body");
        returning = !lerpToPosition(physicalBody.transform.position, 15);
    }

    public void Attack(float angle) {
        if(!attacking){
            attackStart = transform.position;
            Vector2 displacementVector = GetDisplacementVector(angle);
            attackTarget = attackStart + new Vector3(displacementVector.x, 0, displacementVector.y);
            attacking = active = true;
            GetComponent<BoxCollider>().enabled = true;
            GetComponent<BoxCollider>().isTrigger = true;
            GetComponent<MeshRenderer>().enabled = true;
        }
    }

    private Vector2 GetDisplacementVector(float angle) {
        //Cos(theta) = A/H
        float x = Mathf.Cos(angle) * attackRange;
        //Sin(theta) = O/H
        float z = Mathf.Sin(angle) * attackRange;
        return new Vector2(x, z);
    }

    private bool lerpToPosition(Vector3 target, float mySpeed) {
        //Debug.Log("My Speed: " + mySpeed);
        bool reachedTarget = false;
        Vector3 positionThisFrame = transform.position;
        Vector3 path = target - positionThisFrame;
        Vector3 direction = Vector3.Normalize(path);
        Vector3 endMovePos = positionThisFrame + direction * mySpeed * Time.deltaTime;
        if (endMovePos == target || Vector3.Magnitude(endMovePos - positionThisFrame) > Vector3.Magnitude(path)) { 
            //overshot target
            endMovePos = target;
            reachedTarget = true;
        }
        //Debug.Log("Previous position: " + transform.position);
        //Debug.Log("Next Position: " + endMovePos);
        transform.position = endMovePos;
        return reachedTarget;
    }
}
