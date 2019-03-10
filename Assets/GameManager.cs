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

 /*
  * TODO: Allow to start a game with a specific number of players.
  * 
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

    // Tracks how many bodies are currently alive.
    private int bodiesAlive;

    public string menuSceneName;
    public string gameSceneName;

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
        SceneManager.LoadScene(gameSceneName);
        bodiesAlive = 1;
    }


    public void GameOver()
    {
        SceneManager.LoadScene(menuSceneName);
    }


    public void BodyDied(MementoMori body)
    {
        bodiesAlive--;
        if (bodiesAlive <= 0)
            GameOver();
    }
}

