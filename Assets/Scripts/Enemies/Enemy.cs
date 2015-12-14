using UnityEngine;
using UnityEngine.Networking;
using System;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : NetworkBehaviour
{
    public GameObject[] pathPoints;
    public float speed=11.0f;
    public int damage;
    public EnemyStateMachine stateMachine;

    private Rigidbody rb;
    public bool didAttack = false;


    void Start() {
                
        stateMachine = new EnemyStateMachine();
        stateMachine.Start(this);
        rb = gameObject.GetComponent<Rigidbody>();
        rb.maxDepenetrationVelocity = (3.0f);
        
    }

    void OnTriggerEnter(Collider spirit)
    {
        if (spirit.GetComponent<SpiritualBody>() != null)
        {
            stateMachine.waitTime = 3.0f;
        }
    }

    public int getDamage()
    {
        int ret = 0;
        if (!didAttack)
        {
            ret = damage;
            didAttack = true;
        }
        return ret;
    }

    void Update()
    {
        if (!isServer) return;

        //Needs to be a wait time for when the hunting state is about to change.
        Vector3 force = stateMachine.Update();
        force = force * speed;
        force = new Vector3(force.x, 0.0f, force.z);
        if (rb.velocity.magnitude < speed)
        {
            rb.AddForce(force, ForceMode.Acceleration);
        }
        else
        {
            rb.velocity = rb.velocity.normalized * speed;
        }
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.name.Contains("Body"))
        {
            rb.isKinematic = true;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name.Contains("Body"))
        {
            rb.isKinematic = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.name.Contains("Body"))
        {
            rb.isKinematic = false;
        }
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