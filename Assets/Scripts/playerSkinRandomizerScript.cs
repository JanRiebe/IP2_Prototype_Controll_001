using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSkinRandomizerScript
{
    
    static List<int> alreadyAssigned = new List<int>();


    public static void AssignRandomCharacters()
    {       

        // Assigning a unique character to each player.
        foreach (PlayerData p in GameManager.instance.GetAllPlayerData())
        {
            int characterIndex;
            do
            {
                characterIndex = Random.Range(0, GameManager.instance.GetCharacters().Length);
            } while (alreadyAssigned.Contains(characterIndex));

            p.character = GameManager.instance.GetCharacters()[characterIndex];
        }

    }

}
