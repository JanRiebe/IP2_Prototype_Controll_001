/*
 * 
 * The purpose of this script is to manage the game state and provide a central location
 * for data that needs to be passed between scenes.
 * 
 * To do this the script is persistent between scenes
 * and uses the singleton pattern to ensure all scripts access the same GameManager.
 * 
 * It provides some public functions:
 *      StartGame() -> starts a new game by loading the game scene
 *      GameOver()  -> ends the current game and loads the menu scene 
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Access the game manager through its instance.
    public static GameManager instance { get; private set; }

    public string menuSceneName;
    public string gameSceneName;
    
	
	public int numberOfRounds;

    // Central scene independent collection of active players.
    public List<PlayerData> activePlayers;

    Character[] characters;


    private void Awake()
    {
        // Make this a singleton.
        if (instance)
        {
            Destroy(this);
            return;
        }
        instance = this;

        DontDestroyOnLoad(this);

        CreatePlayerDataAtGameStart();

        LoadCharacterObjects();
    }



    void LoadCharacterObjects()
    {
        characters = Resources.LoadAll<Character>("");
    }
    

    public void StartGame()
    {
		// Loading the game scene.
        SceneManager.LoadScene(gameSceneName);
    
    }

    

    public void GameOver()
    {
        // Loading the menu scene.
        SceneManager.LoadScene(menuSceneName);
    }


    public List<PlayerData> GetAllPlayerData()
    {
        return activePlayers;
    }

    public PlayerData GetPlayerData(PlayerAbbr id)
    {
        return activePlayers.Find(p => p.id == id);
    }

    public int GetNumberOfPlayers()
    {
        return activePlayers.Count;
    }

    
    public void CreatePlayerDataAtGameStart()
    {
        activePlayers = new List<PlayerData>();
        int wantedNumberOfPlayers = 2;
        for(int i=0;i<wantedNumberOfPlayers;i++)
        {
            PlayerData p = new PlayerData();
            p.id = (PlayerAbbr)i;
            p.name = p.id.ToString();
            p.score = 0;
            p.character = null;
            activePlayers.Add(p);
        }

    }

    public Character[] GetCharacters()
    {
        return characters;
    }

}

