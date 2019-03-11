using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInGame : MonoBehaviour
{
    
    public GameObject victoryPanel;
    public Text playerName;

    [Tooltip("The per player UI elements.")]
    public List<PlayerStats> playerStats;

    [System.Serializable]
    public class PlayerStats
    {
        public string playerName;
        public Image[] stars;
    }

    [Tooltip("First number is the fade in duration. Second number is how long it stays faded in. Third number is the fade out duration.")]
    public Vector3 starFadeTimes = new Vector3(0.3f, 3.0f, 1.0f);

    [Tooltip("First number is the faded out alpha. Second number is the faded in alpha. Values should be between 0 and 1.")]
    public Vector2 starAlphas = new Vector2(0.3f, 1.0f);
    


    void OnEnable()
    {
        // Subscibing to player score update.
        PlayerInGame.OnScoreUpdated += UpdatePlayerStars;
    }

    void OnDisable()
    {
        // Unsubscibing to player score update.
        PlayerInGame.OnScoreUpdated -= UpdatePlayerStars;
    }


    void UpdatePlayerStars(PlayerInGame player, int score)
    {
        if (score >= 3)
        {
            // Player won
            // Showing win screen
            victoryPanel.SetActive(true);
            playerName.text = player.playerName;
        }


        // Updating stars
        // Finding the stars of the relevant player.
        Image[] stars = playerStats.Find(x => x.playerName == player.playerName).stars;

        for (int i = 0; i < stars.Length; i++)
        {
            if (i < score)
            {
                // Activating the star
                stars[i].gameObject.SetActive(true);
                // Fading in the stars for a time.
                StartCoroutine(FadeImage(stars[i]));
            }
        }
    }
    
	
	public void OnReplayButtonPressed()
	{
		GameManager.instance.StartGame();
	}

	public void OnMenuButtonPressed()
	{
		GameManager.instance.GameOver();
	}



    IEnumerator FadeImage(Image fadeThis)
    {
        // Fade in
        fadeThis.CrossFadeAlpha(starAlphas.y, starFadeTimes.x, false);
        yield return new WaitForSeconds(starFadeTimes.x);
        // Wait
        yield return new WaitForSeconds(starFadeTimes.y);
        // Fade out
        fadeThis.CrossFadeAlpha(starAlphas.x, starFadeTimes.z, false);
        yield return new WaitForSeconds(starFadeTimes.z);
    }
}
