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
            // Assinging random numbers until one is found that is not already assigned.
            int characterIndex;
            do
            {
                characterIndex = Random.Range(0, GameManager.instance.GetCharacters().Length);
            } while (alreadyAssigned.Contains(characterIndex));

            // Ensuring the same character is only assigned once, by saving already assigned characters.
            // But only if there are more or equal players than characters.
            if(GameManager.instance.GetCharacters().Length >= GameManager.instance.GetNumberOfPlayers())
                alreadyAssigned.Add(characterIndex);

            // Writing the character reference into the player data.
            p.character = GameManager.instance.GetCharacters()[characterIndex];
        }

    }

}
