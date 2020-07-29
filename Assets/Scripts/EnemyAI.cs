using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 5f;

    public Transform[] patrolPoints;
    public int currentPatrolPoint;
    
    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;

    public int soundToPlay;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (isProvoked)
        {
            EngageTarget();
        }
        else
        {
            navMeshAgent.SetDestination(patrolPoints[currentPatrolPoint].position);

            if(navMeshAgent.remainingDistance <= .2f)
            {
                currentPatrolPoint++;
                if (currentPatrolPoint >=  patrolPoints.Length)
                {
                    currentPatrolPoint = 0;
                }

                navMeshAgent.SetDestination(patrolPoints[currentPatrolPoint].position);
            }
        }


        if (distanceToTarget <= chaseRange)
        {
            isProvoked = true;
        }
        else if (distanceToTarget >= chaseRange)
        {
            isProvoked = false;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            HealthManager.instance.Hurt();
            AudioManager.instance.PlaySFX(soundToPlay);
        }
    }
}
