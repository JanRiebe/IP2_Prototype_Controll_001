using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSkinRandomizerScript : MonoBehaviour
{
    public static int playerone;
    public static int playertwo;
    
    void Start()
    {
        playerone = Random.Range(1, 6);
        //Debug.Log(playerone);
        playertwo = Random.Range(1, 6);
        //Debug.Log(playertwo);
        //I Know how messy this is, but it's just a really rough draft for it.
        if(playertwo == playerone)
        {
            playertwo = Random.Range(1, 6);
            //Debug.Log(playertwo);
        }
        if (playertwo == playerone)
        {
            playertwo = Random.Range(1, 6);
            //Debug.Log(playertwo);
        }
        if (playertwo == playerone)
        {
            playertwo = Random.Range(1, 6);
            //Debug.Log(playertwo);
        }

    }

    
    void Update()
    {
        
    }
}
