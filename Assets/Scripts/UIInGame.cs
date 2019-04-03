//Scripts
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInGame : MonoBehaviour
{
    
    public GameObject victoryPanel;
    public Text playerName;

    List<PlayerStats> playerStats;

    [System.Serializable]
    class PlayerStats
    {
        public string playerName;
        public Image playerHead;
        public Transform starPanel;
    }

    public Transform playerStatsParent;
    public GameObject playerStatsPrefab;


    [Tooltip("First number is the fade in duration. Second number is how long it stays faded in. Third number is the fade out duration.")]
    public Vector3 starFadeTimes = new Vector3(0.3f, 3.0f, 1.0f);

    [Tooltip("First number is the faded out alpha. Second number is the faded in alpha. Values should be between 0 and 1.")]
    public Vector2 starAlphas = new Vector2(0.3f, 1.0f);
    


    void OnEnable()
    {
        // Subscibing to player score update.
        PlayerInGame.OnScoreUpdated += UpdatePlayerStars;
        PlayerInGame.OnGameOver += OnGameOver;
    }

    void OnDisable()
    {
        // Unsubscibing to player score update.
        PlayerInGame.OnScoreUpdated -= UpdatePlayerStars;
        PlayerInGame.OnGameOver -= OnGameOver;
    }


    private void Start()
    {
        // Assigning the player panels and heads.
        playerStats = new List<PlayerStats>();
        foreach(PlayerData p in GameManager.instance.GetAllPlayerData())
        {
            GameObject go = Instantiate(playerStatsPrefab);
            go.transform.SetParent(playerStatsParent);
            PlayerStats ps = new PlayerStats();
            ps.playerName = p.name;
            ps.playerHead = go.transform.GetChild(0).GetComponent<Image>();
            ps.playerHead.sprite = p.character.face;
            ps.starPanel = go.transform.GetChild(1);
            playerStats.Add(ps);
        }

        // Fading out the player heads at the beggining of the level.
        foreach (PlayerStats p in playerStats)
        {
            StartCoroutine(FadeImage(p.playerHead));
        }
    }



    void OnGameOver(PlayerData winner)
    {
        // Showing win screen
        victoryPanel.SetActive(true);
        playerName.text = winner.name;

        playerStatsParent.gameObject.SetActive(false);
    }




    void UpdatePlayerStars(PlayerData player)
    {
        // Updating stars
        // Finding the stars of the relevant player.
        PlayerStats pStats = playerStats.Find(x => x.playerName == player.name);
        Transform starPanel = pStats.starPanel;


        Image star;
        Color invisible = new Color(0, 0, 0, 0);

        for (int i = 0; i < starPanel.childCount; i++)
        {
            // Getting the star image.
            star = starPanel.GetChild(i).GetComponent<Image>();
            if (i < player.score)
            {
                // Activating the star by making the image visible.
                star.color = Color.white;
                // Fading the star.
                StartCoroutine(FadeImage(star));
            }
            else
                // Deactivating the star by making the image invisible.
                star.color = invisible;
        }

        // Fading the player head.
        StartCoroutine(FadeImage(pStats.playerHead));
        
    }
    
	
	public void OnReplayButtonPressed()
	{
		GameManager.instance.StartGame();
	}

	public void OnMenuButtonPressed()
	{
		GameManager.instance.GameOver();
	}



    IEnumerator FadeImage(Graphic fadeThis)
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
