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

	public delegate void RoundOverData(string winner, string looser, Dictionary<string, int> playerScores);
	public static event RoundOverData OnRoundOver;

	Dictionary<string, int> playerScores = new Dictionary<string, int>();
	
	public int numberOfRounds;

	//TODO manage how the game manager keeps track of players and scores

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

		// Resetting player scores.
		playerScores = new Dictionary<string, int>();

		// Registering to in game events.
		MementoMori.OnDeath += PlayerDied;
		MementoMori.OnVictory += PlayerWon;
    }


	public void RoundOver(MementoMori winner)
	{		
		// Pausing the game.
		Time.timeScale = 0;

		playerScores[winner]++;

		// Sending event telling, that the round is over and who won.
		OnRoundOver(winner.name, playerScores);
	}


    public void GameOver()
    {
		// Unregistering from in game events.
		MementoMori.OnDeath += PlayerDied;
		MementoMori.OnVictory += PlayerWon;

        SceneManager.LoadScene(menuSceneName);
    }


    public void PlayerDied(MementoMori player)
    {
        RoundOver(player);	//TODO needs to be the winner
    }

	public void PlayerWon(MementoMori player)
	{
		RoundOver(player);
	}


	public void StartNextRound()
	{
		SceneManager.LoadScene(gameSceneName);
	}

}

