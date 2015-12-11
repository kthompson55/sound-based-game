using UnityEngine;
using System.Collections;

public class EnemyStateMachine{

    public bool seeAPlayer=false;
    public Vector3 lastKnownLocation=Vector3.zero;
    public State currentState;
    public Enemy owner;
    private string lastState;
    public Rigidbody rb;
    public float waitTime = 0.0f;

	// Use this for initialization
	public void Start (Enemy _owner) {
        owner = _owner;

        currentState = new PatrolState();
        currentState.Init(this);
        lastState = currentState.GetName();
        rb = owner.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	public Vector3 Update () {
        Vector3 ret = Vector3.zero;
        if (waitTime <= 0.0f)
        {
            rb.isKinematic = false;
            waitTime = 0.0f;
            if (owner != null)
            {
                if (seeAPlayer)
                {
                    if (currentState.GetStateEnum() != EnumStates.Attack)
                    {
                        currentState = new AttackState();
                        currentState.Init(this);
                    }
                }
                else if (!seeAPlayer && lastKnownLocation != Vector3.zero)
                {
                    if (currentState.GetStateEnum() != EnumStates.Hunt)
                    {
                        currentState = new HuntState();
                        currentState.Init(this);
                    }
                }
                else
                {
                    if (currentState.GetStateEnum() != EnumStates.Patrol)
                    {
                        currentState = new PatrolState();
                        currentState.Init(this);
                    }
                }
                ret = currentState.RunState(Time.deltaTime);
            }
        }
        else
        {
            waitTime -= Time.deltaTime;
            if (owner.didAttack && waitTime <= 0)
            {
                owner.didAttack = false;
                rb.isKinematic = false;
            }
            rb.isKinematic = true;
        }
        return ret;
	}
}

public enum EnumStates
{
    Patrol,
    Hunt,
    Attack
}

public interface State
{
    bool Init(EnemyStateMachine sm);
    Vector3 RunState(float dt);
    string GetName();
    EnumStates GetStateEnum();
}

//When we see a player
public class AttackState : State
{
    EnemyStateMachine owner;
    Vector3 targetLocation;
    float attackTimePause = 2.0f;

    //Chase the player to a certain radius then pause
    public Vector3 RunState(float dt)
    {
        Vector3 ret = Vector3.zero;
        if (owner != null)
        {
            if (owner.owner.didAttack)
            {
                owner.waitTime = attackTimePause;
            }
            Vector3 dir = targetLocation - owner.owner.transform.position;
            dir.Normalize();
            ret = dir;
        }
        return ret;
    }

    public bool Init(EnemyStateMachine sm)
    {
        owner = sm;
        targetLocation = owner.lastKnownLocation;
        return true;
    }

    public string GetName()
    {
        return "Attack";
    }

    public EnumStates GetStateEnum()
    {
        return EnumStates.Attack;
    }

    private bool WithinARadius(Vector3 one, Vector3 two, float radius)
    {
        bool ret = false;
        ret = (one - two).magnitude <= radius;
        return ret;
    }

}

//When we don't see a player
public class PatrolState : State
{
    EnemyStateMachine owner;
    private int patrolLocation = 0;
    private int lastLocation;
    private Vector3 directionTo;
    private bool forceNext = false;

    public bool Init(EnemyStateMachine sm)
    {
        owner = sm;
        lastLocation = owner.owner.pathPoints.Length-1;

        if (sm != null)
        {
            Debug.Log(lastLocation);
            if (lastLocation > 0)
            {
                directionTo = (owner.owner.pathPoints[0].transform.position - owner.owner.pathPoints[1].transform.position);
                directionTo.Normalize();
            }
            return true;
        }
        else
            return false;
    }

    //Patrols the area until a player is spotted
    public Vector3 RunState(float dt)
    {
        Vector3 ret = Vector3.zero;
        if (owner != null)
        {
            if (lastLocation != -1)
            {
                if (( WithinARadius(owner.owner.pathPoints[patrolLocation].transform.position, owner.owner.transform.position,1.0f)))
                {
                    //move to next location
                    patrolLocation += 1;
                    if (patrolLocation >= owner.owner.pathPoints.Length)
                    {
                        patrolLocation = 0;
                    }
                }
                Vector3 dir = owner.owner.pathPoints[patrolLocation].transform.position-owner.owner.transform.position;
                dir.Normalize();
                ret = dir;
            }
        }
        return ret;
    }

    private bool WithinARadius(Vector3 one, Vector3 two, float radius)
    {
        bool ret = false;
        ret = (one - two).magnitude <= radius;
        return ret;
    }

    public string GetName()
    {
        return "Patrol";
    }

    public EnumStates GetStateEnum()
    {
        return EnumStates.Patrol;
    }

}

//When we saw a player then lost them
public class HuntState : State
{
    EnemyStateMachine owner;
    Vector3 targetLocation;
    public bool Init(EnemyStateMachine sm)
    {
        owner = sm;
        targetLocation = owner.lastKnownLocation;
        return true;
    }

    //Go to a last known location of the player
    public Vector3 RunState(float dt)
    {
        Vector3 ret = Vector3.zero;
        if (owner != null)
        {
            if ((WithinARadius(targetLocation, owner.owner.transform.position, 1.5f)))
            {
                if (!owner.seeAPlayer)
                {
                    owner.lastKnownLocation = Vector3.zero;
                    owner.waitTime = 1.0f;
                }
            }
            else
            {
                targetLocation = owner.lastKnownLocation;
                Vector3 dir = targetLocation - owner.owner.transform.position;
                dir.Normalize();
                ret = dir;
            }
        }
        return ret;
    }

    public string GetName()
    {
        return "Hunt";
    }

    public EnumStates GetStateEnum()
    {
        return EnumStates.Hunt;
    }

    private bool WithinARadius(Vector3 one, Vector3 two, float radius)
    {
        bool ret = false;
        ret = (one - two).magnitude <= radius;
        return ret;
    }

}
