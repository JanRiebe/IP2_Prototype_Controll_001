using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSpriteManager : MonoBehaviour
{
    public Sprite[] spriteList;
    public int playernumber;
    bool setup = false;


    void Start()
    {
        
    }


    void Update()
    {
        if (!setup)
        {
            if (playernumber == 1)
            {
                this.GetComponent<SpriteRenderer>().sprite = spriteList[playerSkinRandomizerScript.playerone];
            }
            if (playernumber == 2)
            {
                this.GetComponent<SpriteRenderer>().sprite = spriteList[playerSkinRandomizerScript.playertwo];
            }
            setup = true;
        }
    }
}
