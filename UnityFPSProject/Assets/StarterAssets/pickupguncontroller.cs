using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupguncontroller : MonoBehaviour
{

    public ProjectileGunTutorial gunscript;
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, gunContainer, fpscam;

    public float pickupRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;




    // Start is called before the first frame update
    void Start()
    {
        if (!equipped)
        {
            gunscript.enabled = false;
            rb.isKinematic = false;
            coll.isTrigger = false;
            slotFull = false;
        }
        if (equipped)
        {
            gunscript.enabled = false;
            rb.isKinematic = false;
            coll.isTrigger = false;
            slotFull = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distancetoPlayer = player.position - transform.position;
        if (!equipped && distancetoPlayer.magnitude <= pickupRange && Input.GetKeyDown(KeyCode.E) && !slotFull) PickUp();

        if (equipped && Input.GetKeyDown(KeyCode.Q)) Drop();
    }

    private void PickUp()
    {
        equipped = true;
        slotFull = true;

        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;




        //make rigid body kinematic so the gun does not move anymore
        rb.isKinematic = true;
        coll.isTrigger = true;


        gunscript.enabled = true;
    }

    private void Drop()
    {
        equipped = false;
        slotFull = false;

        //set [arent to null since gun no longer equipped
        transform.SetParent(null);

        //make rigid body kinematic so the gun does not move anymore
        rb.isKinematic = false;
        coll.isTrigger = false;

        // have gun carry momentum of player when discarded
        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        //add force

        rb.AddForce(fpscam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpscam.up * dropForwardForce, ForceMode.Impulse);
        //add random rotation
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);




        gunscript.enabled = false;
    }

    


}
