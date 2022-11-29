using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemybulletscript : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject explosion;
    public LayerMask whatIsPlayer;
    private GameObject impact;


    //stats
    [Range(0f, 1f)]
    public float bounciness;
    public bool useGravity;

    public int explosionDamage;
    public float explosionRange;


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
        if (collision.collider.CompareTag("EnemyBullet")) return;
        collisions++;

        if (collision.collider.CompareTag("Player") && explodenTouch) Explode();
    }


    private void Explode()
    {
        if (explosion != null) impact = Instantiate(explosion, transform.position, Quaternion.identity);
        soundmanager.PlaySound("explosion");
        //check for enemies
        Collider[] players = Physics.OverlapSphere(transform.position, explosionRange, whatIsPlayer);

        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].CompareTag("Player"))
            {
                players[i].GetComponent<StarterAssets.StarterAssetsInputs>().TakeDamage(explosionDamage);
            }
        }


        Invoke("Delay", 0.05f);

        Destroy(impact, 4f);





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
