using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class powerbarscript : MonoBehaviour
{

    public GameObject scoreBox;

    float speed = 100.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag.Equals("ak"))
        {
            // scoreboard.currentPowerScore += 10;
            col.GetComponent<ProjectileGunTutorial>().magazineSize+=10;
            soundmanager.PlaySound("barsound");
            Destroy(gameObject);
           
        }
        if (col.gameObject.tag.Equals("rpg"))
        {
            // scoreboard.currentPowerScore += 10;
            col.GetComponent<ProjectileGunTutorial>().magazineSize += 1;
            soundmanager.PlaySound("barsound");
            Destroy(gameObject);

        }

    }
}