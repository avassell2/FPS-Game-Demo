using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : enemyAIscript
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
            animator.SetBool("move_forward", true);
        }
        else
        {
            animator.SetBool("move_forward", false);
        }
        if (playerInSightRange && !playerInAttackRange && playerstate.isPower == false)
        {
            ChasePlayer();
            animator.SetBool("move_forward_fast", true);
        }
        else
        {
            animator.SetBool("move_forward_fast", false);
        }
        if (playerInSightRange && !playerInAttackRange && playerstate.isPower == true) Flee();
        if (playerInAttackRange && playerInSightRange)
        {
           // AttackPlayer();
            animator.SetBool("attack_short_001", true);
        }
        else
        {
            animator.SetBool("attack_short_001", false);
        }
    }



    public override void TakeDamage(int damage)
    {
        health -= damage;
        soundmanager.PlaySound("guarddeath");
        animator.Play("damage_001", 0, 0);






        if (health <= 0)
        {
            animator.Play("dead", 0, 0);
            Invoke(nameof(DestroyEnemy), 0.5f);
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
            animator.SetBool("move_forward", false);
        }
        else
        {
            //animator.SetBool("walk", true);
        }

    }




   // public void AttackPlayer()
   // {
   //     base.AttackPlayer();

   // }




}

