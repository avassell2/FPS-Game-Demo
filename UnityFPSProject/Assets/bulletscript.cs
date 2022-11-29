using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletscript : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject explosion;
    public LayerMask whatIsEnemies;
    private GameObject imapct;

    //stats
    [Range(0f, 1f)]
    public float bounciness;
    public bool useGravity;

    public int explosionDamage;
    public float explosionRange;
    public float explosionForce;

    private GameObject impact;

    //lifetime
    public int maxCollision;
    public float maxLifetimel;
    public bool explodenTouch = true;

    int collisions;
    PhysicMaterial physics_mat;

    // Start is called before the first frame update
    void Start()
    {
        setup();
    }

    // Update is called once per frame
    void Update()
    {
        if (collisions > maxCollision) Explode();


        //countdown life time
        maxLifetimel -= Time.deltaTime;
        if (maxLifetimel <= 0) Explode();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet")) return;
        collisions++;

        if (collision.collider.CompareTag("enemy") && explodenTouch) Explode();
    }


    private void Explode()
    {
       if (explosion != null) impact = Instantiate(explosion, transform.position, Quaternion.identity);
        soundmanager.PlaySound("explosion");
        //check for enemies
        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange, whatIsEnemies);
        
            for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].CompareTag("enemy"))
            {
                enemies[i].GetComponent<enemyAIscript>().TakeDamage(explosionDamage);


            }
            
        }


        Invoke("Delay",0.05f);
       
        Destroy(impact, 1f);





    }

    private void Delay()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }


    private void setup()
    {
        physics_mat = new PhysicMaterial();
        physics_mat.bounciness = bounciness;
        physics_mat.frictionCombine = PhysicMaterialCombine.Minimum;
        physics_mat.bounceCombine = PhysicMaterialCombine.Maximum;
        GetComponent<SphereCollider>().material = physics_mat;

        rb.useGravity = useGravity;
    }


}
