using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gamewin : MonoBehaviour
{
    private float targetTime = 5.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        targetTime -= Time.deltaTime;
        if (targetTime <= 0.0f)
        {
            timerEnded();
        }
    }

    public void setup()
    {
        gameObject.SetActive(true);
    }

    void timerEnded()
    {
        //do your stuff here.
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);

    }

}
