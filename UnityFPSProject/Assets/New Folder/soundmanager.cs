using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundmanager : MonoBehaviour
{ 

 public static AudioClip playerdamage, chestpick, barpick, enemydeath,expl,gunfire,moan;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        playerdamage = Resources.Load<AudioClip>("mindamage");
        enemydeath = Resources.Load<AudioClip>("guarddeath");
        chestpick = Resources.Load<AudioClip>("chestsound");
        barpick = Resources.Load<AudioClip>("barsound");
        expl = Resources.Load<AudioClip>("explosion");
        gunfire = Resources.Load<AudioClip>("gunfireAK");
        moan = Resources.Load<AudioClip>("moan");


        audioSrc = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "mindamage":
                audioSrc.PlayOneShot(playerdamage);
                break;

            case "guarddeath":
                audioSrc.PlayOneShot(enemydeath);
                break;

            case "chestsound":
                audioSrc.PlayOneShot(chestpick);
                break;

            case "barsound":
                audioSrc.PlayOneShot(barpick);
                break;
            case "gunfireAK":
                audioSrc.PlayOneShot(gunfire);
                break;

            case "explosion":
                audioSrc.PlayOneShot(expl);
                break;

            case "moan":
                audioSrc.PlayOneShot(moan);
                break;


        }
    }

    
}
