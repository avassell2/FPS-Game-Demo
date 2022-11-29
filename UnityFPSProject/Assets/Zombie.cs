using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : enemyAIscript
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
     void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);


        if (!playerInSightRange && !playerInAttackRange)
        {
            Patroling();
            animator.SetBool("Z_Walk_InPlace", true);
        }
        else
        {
            animator.SetBool("Z_Walk_InPlace", false);
        }
        if (playerInSightRange && !playerInAttackRange && playerstate.isPower == false)
        {
            ChasePlayer();
            animator.SetBool("Z_Run_InPlace", true);
        }
        else
        {
            animator.SetBool("Z_Run_InPlace", false);
        }
        if (playerInSightRange && !playerInAttackRange && playerstate.isPower == true) Flee();
        if (playerInAttackRange && playerInSightRange)
        {
            AttackPlayer();
            animator.SetBool("Z_Attack", true);
            //soundmanager.PlaySound("moan");
        }
        else
        {
            animator.SetBool("Z_Attack", false);
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
            animator.SetBool("Z_Walk_InPlace", false);
        }
        else
        {
            //animator.SetBool("walk", true);
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





    public void zombieattack()
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

