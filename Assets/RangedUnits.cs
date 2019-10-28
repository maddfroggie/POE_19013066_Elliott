using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class RangeUnit : LivingEntity
{
    public enum State
    {
        Idle = 0,
        Chasing = 1,
        Attacking = 2
    }

    [SerializeField]
    private float AttackDamage = 1;
    [SerializeField]
    private float AttackDistance = .8f;
    [SerializeField]
    private float AttackTime = 1;
    [SerializeField]
    private float UpdatePathRate = 1;
    [SerializeField]
    private Transform target;


    private NavMeshAgent pathFinder;
    private Material material;
    private Color originalColour;

    private float nextAttackTime;
    private State state;
    private bool hasTarget;

    private float myCollisionRaduis;
    private float targetCollisionRaduis;
    protected override void Start()
    {
        base.Start();
        myCollisionRaduis = GetComponent<CapsuleCollider>().radius;
        pathFinder = GetComponent<NavMeshAgent>();
        if (target != null)
        {
            //Get Player
            hasTarget = true;
            state = State.Chasing;

            //Get colliders
            targetCollisionRaduis = target.gameObject.GetComponent<CapsuleCollider>().radius;

            //Begin pathfinding
            StartCoroutine(UpdatePath());
        }
        else
        {
            hasTarget = false;
            state = State.Idle;
        }
    }


    protected override void Update()
    {
        base.Update();
    }


    public void SetTarget(Transform target)
    {
        hasTarget = true;
        state = State.Chasing;
        this.target = target;

        //Get colliders
        targetCollisionRaduis = target.gameObject.GetComponent<CapsuleCollider>().radius;

        //Begin pathfinding
        StartCoroutine(UpdatePath());
    }

    public State GetState()
    {
        return state;
    }


    IEnumerator UpdatePath()
    {
        float refreshRate = UpdatePathRate;
        while (hasTarget)
        {
            if (state == State.Chasing)
            {
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);
                targetPosition -= dirToTarget * (myCollisionRaduis + targetCollisionRaduis + AttackDistance / 2);
                if (pathFinder != null)
                {
                    pathFinder.SetDestination(targetPosition);
                }
            }
            yield return new WaitForSeconds(refreshRate);
        }
    }


}
