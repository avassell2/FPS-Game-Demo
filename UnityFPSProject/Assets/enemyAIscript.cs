
using UnityEngine;
using UnityEngine.AI;

public class enemyAIscript : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    public Animator animator;

    public bool explodenTouch = true;

    protected GameObject impact;


    public StarterAssets.StarterAssetsInputs playerstate;

    public int explosionDamage;

    public float explosionRange;

    //Patroling
    public Vector3 walkPoint;
    protected private bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    protected bool alreadyAttacked;
    public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("PlayerCapsule").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

     
    }

    protected virtual void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
            
        }
        else
        {
            //animator.SetBool("walk", true);
        }

    }
    public void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }

        agent.SetDestination(walkPoint);

    }

    public void ChasePlayer()
    {
        
            agent.SetDestination(player.position);

        
       

    }

    public void Flee()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        


            Vector3 dirtoplayer = transform.position - player.transform.position;

            //Vector3 newPos = transform.position + player.transform.position;

            agent.SetDestination(dirtoplayer);
            
    }

    protected virtual void AttackPlayer()
    {
    //Make sure enemy doesn't move
     agent.SetDestination(transform.position);

     transform.LookAt(player);

    if (!alreadyAttacked)
     {
            //Attack code here
            soundmanager.PlaySound("gunfireAK");
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
     rb.AddForce(transform.forward * 300, ForceMode.Impulse);
     rb.AddForce(transform.up * 0, ForceMode.Impulse);


            //currentBullet.transform.forward = directionWithSpread.normalized;

            //Add forces to bullet
            //currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
            //currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);
            ///End of attack code

            alreadyAttacked = true;
            
            Invoke(nameof(ResetAttack), timeBetweenAttacks);

        }
        

       
    }
    public void ResetAttack()
    {
        alreadyAttacked = false;

        
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        soundmanager.PlaySound("guarddeath");
        animator.Play("damage",0,0);






        if (health <= 0)
        {
           
            Invoke(nameof(DestroyEnemy), 0.5f);
        }
    }
    protected virtual void DestroyEnemy()
    {
        playerstate.killscore();
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
