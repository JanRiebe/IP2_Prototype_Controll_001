using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultsMenu : MonoBehaviour
{

	public GameObject menuPanel;
	public Text winnerName;
	public GameObject[] winnerStars;
	public Text looserName;
	public GameObject[] looserStars;
	public GameObject nextRoundButton;
	public GameObject mainMenuButton;


	private void Awake()
	{
		// Subscibing to round over event.
		GameManager.OnRoundOver += Show;
	}


	public void Show(string winner, Dictionary<string, int> playerScores)
	{
		menuPanel.active = true;

		// Setting the winner name and stars.
		winnerName.text = winner;
		for(int i=0; i<winnerStars.Length; i++)
		{
			winnerStars[i].active = i < playerScores[winner];
		}

		// Setting the looser name and stars.
		foreach(string player in playerScores.Keys)
		{
			if(player != winner)
			{
				looserName.text = player;
				for(int i=0; i < looserStars.Length; i++)
				{
					looserStars[i].active = i < playerScores[player];
				}
				break;
			}				
		}

		// Activating the right button for continuing.
		bool reachedMaxRounds = playerScores[winner] >= GameManager.instance.numberOfRounds;
		nextRoundButton.active = !reachedMaxRounds;
		mainMenuButton.active = reachedMaxRounds;
		
	}
	
	public void OnNextRoundButtonPressed()
	{
		GameManager.instance.StartNextRound();
	}

	public void OnMenuButtonPressed()
	{
		GameManager.instance.GameOver();
	}
}
