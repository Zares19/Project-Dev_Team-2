using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class EnemyScript : MonoBehaviour
{
    NavMeshAgent navAgent;
    Transform _player;
    Animator _anim;
    public LayerMask whatisGround, whatIsPlayer;
    public Transform projectileSpawnPoint;
    public Rigidbody enemyProjectile;

    // Melee Hit Box
    public GameObject meleeHitBox;

    //Type of Enemy
    public bool meleeEnemy, projectileEnemy, lungeEnemy;

    //Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool isAttacking;


    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;


    // Start is called before the first frame update
    private void Awake()
    {
        _player = GameObject.Find("Player").transform;
        _anim = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        meleeHitBox.SetActive(false);
    }

    private void Update()
    {
        //Check for sigh range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();

    }

    void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet) navAgent.SetDestination(walkPoint);
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f) walkPointSet = false;
    }

    void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatisGround)) walkPointSet = true;
    }
    
    void ChasePlayer()
    {
        navAgent.SetDestination(_player.position);
    }

    void AttackPlayer()
    {
        navAgent.SetDestination(transform.position);
        transform.LookAt(_player);

        if(!isAttacking)
        {
            if(meleeEnemy)
            {
                //_anim.SetTrigger("Attack");
                StartCoroutine(MeleeAttackHit());

            }
            if(projectileEnemy)
            {
                Rigidbody _projectileRB;
                _projectileRB = Instantiate(enemyProjectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation) as Rigidbody;
                _projectileRB.AddForce(projectileSpawnPoint.forward * 10f, ForceMode.Impulse);
            }
            if(lungeEnemy)
            {
                // We will use DoTween for this
                StartCoroutine(LungeAttack());
            }
            
            isAttacking = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    void ResetAttack()
    {
        isAttacking = false;
    }

    IEnumerator MeleeAttackHit()
    {
        meleeHitBox.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        meleeHitBox.SetActive(false);
    }

    IEnumerator LungeAttack()
    {
        Vector3 lungeTarget = _player.position;
        yield return new WaitForSeconds(0.2f);
        transform.DOJump(lungeTarget, 2.25f, 1, 0.5f);
    }
}
