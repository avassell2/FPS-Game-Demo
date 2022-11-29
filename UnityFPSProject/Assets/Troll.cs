using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll : enemyAIscript
{
    //public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if(animator.GetBool("attack03") == true)
        {
           agent.SetDestination(transform.position);
        }

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patroling();
            animator.SetBool("walk", true);
        }
        else
        {
            animator.SetBool("walk", false);
        }
        if (playerInSightRange && !playerInAttackRange && playerstate.isPower == false)
        {
            ChasePlayer();
            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
        }
        if (playerInSightRange && !playerInAttackRange && playerstate.isPower == true) Flee();
        if (playerInAttackRange && playerInSightRange)
        {
            AttackPlayer();
            animator.SetTrigger("attack04");
            animator.SetBool("attack03", true);
        }
        else
        {
            animator.SetBool("attack03", false);
        }
    }


    protected override void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //Attack code here

            //soundmanager.PlaySound("gunfireAK");
            //Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            //rb.AddForce(transform.forward * 300, ForceMode.Impulse);
            //rb.AddForce(transform.up * 0, ForceMode.Impulse);

           // Explode();


            //currentBullet.transform.forward = directionWithSpread.normalized;

            //Add forces to bullet
            //currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
            //currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);
            ///End of attack code

            alreadyAttacked = true;

            Invoke(nameof(ResetAttack), timeBetweenAttacks);

        }

    }



    protected override void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
            animator.SetBool("walk", false);
        }
        else
        {
            //animator.SetBool("walk", true);
        }

    }









    //private void OnCollisionEnter(Collision collision)
   // {
       

    //    if (collision.collider.CompareTag("Player") && explodenTouch) Explode();
   // }


    public void trollattack()
    {
       // if (explosion != null) impact = Instantiate(explosion, transform.position, Quaternion.identity);
        soundmanager.PlaySound("explosion");
        //check for enemies
        Collider[] players = Physics.OverlapSphere(transform.position, attackRange, whatIsPlayer);

        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].CompareTag("Player"))
            {
                players[i].GetComponent<StarterAssets.StarterAssetsInputs>().TakeDamage(explosionDamage);


                Rigidbody rb = players[i].GetComponent<Rigidbody>();
                rb.AddForce(new Vector3(1.5f, 1.5f, 1.5f));





            }
        }


        //Invoke("Delay", 0.05f);

       // Destroy(impact, 4f);





    }

    //private void Delay()
    //{
     //   Destroy(gameObject);
    //}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }





















}
