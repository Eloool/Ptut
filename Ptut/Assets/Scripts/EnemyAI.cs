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

    private bool hasDestination;//if he has destination, he will go for it
    private bool isAttacking;//when this boolean is tru that is wait the time of the attacking animation


    private void Start()
    {
        playerStats = PlayerStats.instance;
        player =  playerStats.gameObject.transform;
    }

    public void StopMoving()
    {
        if (agent.enabled)
        {
            agent.isStopped = true; // Arr�te le d�placement
            agent.ResetPath();      // Efface le chemin actuel de l'agent
        }

        animator.SetFloat("Speed", 0f); // Met � jour l'animation pour indiquer un arr�t
        isAttacking = false;           // Assure que l'ennemi n'est pas en train d'attaquer
        hasDestination = false;        // R�initialise l'�tat de destination
    }

    // Update is called once per frame
    void Update()
    {
        EnnemiInteractable ene = GetComponent<EnnemiInteractable>();
        if (ene.health <= 0f)
        {
            StopMoving();
            return;
        }
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

            Vector3 fleeDirection = (transform.position - player.position).normalized;//Direction oppos� au joueur 
            Vector3 fleePosition = transform.position + fleeDirection * fleeDistance;//hit une autre position oppos� au joueur

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
        if (playerStats.isDying) yield break; // Exit coroutine if the player is dying

        isAttacking = true;
        agent.isStopped = true;

        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(attackDelay / 2); // separated delay so the player takes damage in the middle of the animation/attackDelay

        playerStats.TakeDamage(damageDealt); // Deal damage to the player

        yield return new WaitForSeconds(attackDelay / 2);

        if (agent.enabled)
        {
            agent.isStopped = false;
        }

        isAttacking = false;
    }


    private void OnDrawGizmos()//affiche dans la sc�ne les diff�rents les rayons d'actions de l'IA
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
