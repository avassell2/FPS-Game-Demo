using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour
{
    public GameObject scoreBox;
    public StarterAssets.StarterAssetsInputs player;

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
        if(col.gameObject.tag.Equals("Player"))
        player.lives += 20;
        soundmanager.PlaySound("barsound");
        Destroy(gameObject);

    }
}
