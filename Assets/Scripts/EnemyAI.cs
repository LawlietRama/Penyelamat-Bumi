using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float attackRange = 1f;

    public Transform[] patrolPoints;
    public int currentPatrolPoint;
    
    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;

    public int soundToPlay;

    public enum AIState
    {
        isIdle, isPatrolling, isChasing, isAttacking
    };
    public AIState currentState;

    public float waitAtPoint = 2f;
    public float waitCounter;

    public float timeBetweenAttacks = 2f;
    public float attackCounter;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        waitCounter = waitAtPoint;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);

        switch (currentState)
        {
            case AIState.isIdle:
                //set animation moving to false
                if (waitCounter > 0)
                {
                    waitCounter -= Time.deltaTime;
                }
                else
                {
                    currentState = AIState.isPatrolling;
                    navMeshAgent.SetDestination(patrolPoints[currentPatrolPoint].position);
                }

                if (distanceToTarget <= chaseRange)
                {
                    currentState = AIState.isChasing;
                    //set animation moving
                }
                break;

            case AIState.isPatrolling:
                //navMeshAgent.SetDestination(patrolPoints[currentPatrolPoint].position);

                if (navMeshAgent.remainingDistance <= .2f)
                {
                    currentPatrolPoint++;
                    if (currentPatrolPoint >= patrolPoints.Length)
                    {
                        currentPatrolPoint = 0;
                    }

                    //navMeshAgent.SetDestination(patrolPoints[currentPatrolPoint].position);
                    currentState = AIState.isIdle;
                    waitCounter = waitAtPoint;
                }

                if (distanceToTarget <= chaseRange)
                {
                    currentState = AIState.isChasing;
                }
                //set animation moving true
                break;

            case AIState.isChasing:
                ChaseTarget();

                if(distanceToTarget <= attackRange)
                {
                    currentState = AIState.isAttacking;
                    //set animation attack
                    //->anim.SetTrigger("Attack");
                    //anim.SetBool("IsMoving", false);

                    navMeshAgent.velocity = Vector3.zero;
                    navMeshAgent.isStopped = true;

                    attackCounter = timeBetweenAttacks;
                }

                if(distanceToTarget > chaseRange)
                {
                    currentState = AIState.isIdle;
                    waitCounter = waitAtPoint;

                    navMeshAgent.velocity = Vector3.zero;
                    navMeshAgent.SetDestination(transform.position);
                }

                break;

            case AIState.isAttacking:

                transform.LookAt(target.transform, Vector3.up);
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

                attackCounter -= Time.deltaTime;
                if(attackCounter <= 0)
                {
                    if(distanceToTarget < attackRange)
                    {
                        //anime.SetTrigger("Attack");
                        attackCounter = timeBetweenAttacks;
                    }
                    else
                    {
                        currentState = AIState.isIdle;
                        waitCounter = waitAtPoint;

                        navMeshAgent.isStopped = false;
                    }
                }

                break;
        }
    }

    private void EngageTarget()
    {
        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        else if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void ChaseTarget()
    {
        navMeshAgent.SetDestination(target.position);
    }

    private void AttackTarget()
    {

        Debug.Log(name + " menyerang "+ target.name);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            HealthManager.instance.Hurt();
            AudioManager.instance.PlaySFX(soundToPlay);
        }
    }*/
}
