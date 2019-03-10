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
using System.Linq;

public class GameManager : MonoBehaviour
{
    // Access the game manager through its instance.
    public static GameManager instance { get; private set; }

    public string menuSceneName;
    public string gameSceneName;
    
	
	public int numberOfRounds;

	

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


    
}

