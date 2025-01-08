using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Transform player;

    [SerializeField]
    private PlayerStats playerStats;

    [SerializeField]
    private NavMeshAgent agent;

    [SerializeField]
    private Animator animator;

    [Header("Stats")]
    [SerializeField]
    private float detectionRadius;

    [SerializeField]
    private float walkSpeed;

    [SerializeField]
    private float chaseSpeed;

    [SerializeField]
    private float attackRadius;

    [SerializeField]
    private float attackDelay;

    [SerializeField]
    private float damageDealt;

    [SerializeField]
    private float rotationSpeed;

    [Header("Wandering parameters")]
    [SerializeField]
    private float wanderingWaitTimeMin;
    [SerializeField]
    private float wanderingWaitTimeMax;

    [SerializeField]
    private float wanderingDistanceMin;
    [SerializeField]
    private float wanderingDistanceMax;

    [Header("Behavior")]
    [SerializeField]
    private bool isEnemy = true; // Determines if the AI is an enemy or afraid

    [SerializeField]
    private float fleeDistance = 10f; // Distance to flee when afraid

    private bool hasDestination;
    private bool isAttacking;

    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (isEnemy)
        {
            HandleEnemyBehavior();
        }
        else
        {
            HandleFearfulBehavior();
        }

        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    private void HandleEnemyBehavior()
    {
        if (Vector3.Distance(player.position, transform.position) < detectionRadius)
        {
            agent.speed = chaseSpeed;

            Quaternion rot = Quaternion.LookRotation(player.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotationSpeed * Time.deltaTime);

            if (!isAttacking)
            {
                if (Vector3.Distance(player.position, transform.position) < attackRadius)
                {
                    StartCoroutine(AttackPlayer());
                }
                else
                {
                    agent.SetDestination(player.position);
                }
            }
        }
        else
        {
            Wander();
        }
    }

    private void HandleFearfulBehavior()
    {
        if (Vector3.Distance(player.position, transform.position) < detectionRadius)
        {
            agent.speed = chaseSpeed;

            Vector3 fleeDirection = (transform.position - player.position).normalized;//Direction opposé au joueur 
            Vector3 fleePosition = transform.position + fleeDirection * fleeDistance;//hit une autre position opposé au joueur

            NavMeshHit hit;
            if (NavMesh.SamplePosition(fleePosition, out hit, fleeDistance, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }
        }
        else
        {
            Wander();
        }
    }

    private void Wander()
    {
        agent.speed = walkSpeed;

        if (agent.remainingDistance < 0.75f && !hasDestination)
        {
            StartCoroutine(GetNewDestination());
        }
    }

    IEnumerator GetNewDestination()
    {
        hasDestination = true;
        yield return new WaitForSeconds(Random.Range(wanderingWaitTimeMin, wanderingWaitTimeMax));

        Vector3 nextDestination = transform.position;
        nextDestination += Random.Range(wanderingDistanceMin, wanderingDistanceMax) * new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(nextDestination, out hit, wanderingDistanceMax, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
        hasDestination = false;
    }

    IEnumerator AttackPlayer()
    {
        isAttacking = true;
        agent.isStopped = true;

        animator.SetTrigger("Attack");

        playerStats.TakeDamage(damageDealt);//Ne pas oublier de mettre les stats dans le serialized playerStats
        Debug.Log("joueur touché");
        yield return new WaitForSeconds(attackDelay);

        if (agent.enabled)
        {
            agent.isStopped = false;
        }

        isAttacking = false;
    }

    private void OnDrawGizmos()//affiche dans la scène les différents les rayons d'actions de l'IA
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
