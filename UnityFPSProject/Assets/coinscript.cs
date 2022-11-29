using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coinscript : MonoBehaviour{

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

    void OnTriggerEnter()
    {
        scoreboard.currentScore += 1;
        soundmanager.PlaySound("chestsound");
        Destroy(gameObject);
    }
}
