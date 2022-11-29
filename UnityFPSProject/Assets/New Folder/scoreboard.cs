using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreboard : MonoBehaviour
{

    public GameObject scoreBox;
    public static int currentScore;
    public int internalScore;
    public gamewin gmscreen;


    public StarterAssets.StarterAssetsInputs player;
    public GameObject scoreBoxPower;
    public static int currentPowerScore;
    public int internalPowerScore;
    

    private float targetTime = 10.0f;




    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
        internalScore = 0;
        currentPowerScore = 0;
        internalPowerScore = 0;
}

    // Update is called once per frame
    void Update()
    {

        internalScore = currentScore;
        scoreBox.GetComponent<Text>().text = "" + internalScore;

        internalPowerScore = currentPowerScore;
        scoreBoxPower.GetComponent<Text>().text = "" + internalPowerScore;

        if (player.kills >= 14)
        {
            GameWin();
        }

        if (currentPowerScore >= 30)
        {
           
            player.isPower = true;
            

        }

        if (targetTime <= 0)
        {
            //currentPowerScore = 0;
            player.isPower = false;
            targetTime = 10f;
            for (int i = 30; i > 0; i--)
            {
                --currentPowerScore;
            }
        }

        //if (currentPowerScore == 100)
        //{
        //  scoreboard.currentScore += 1;
        //}
        if (player.isPower == true)
        {
           
            targetTime -= Time.deltaTime;
            
        }


    }

    public void GameWin()
    {
        gmscreen.setup();
    }

}
