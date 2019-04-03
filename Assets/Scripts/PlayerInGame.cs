using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerInGame : MonoBehaviour
{
    PlayerAbbr id;

    PlayerData data;

    delegate void TellThemWhoDidIt(PlayerInGame sender);
	static event TellThemWhoDidIt OnDeath;

    public delegate void SendPlayer(PlayerData player);
    public static event SendPlayer OnScoreUpdated;

    public static event SendPlayer OnGameOver;

    public delegate void EmptyDelegate();
    public static event EmptyDelegate OnRoundOver;


    private void Start()
    {
        // Gettin my id from the input manager. (I assume it won't be forgotten to set it there, cause else the characters won't respond.)
        id = transform.parent.GetComponent<InputManager>().playerAbbreviation;
        // Getting the player data from the game manager.
        data = GameManager.instance.GetPlayerData(id);
        // Resetting the score at beginning of level.
        data.score = 0;

        Debug.Log("I'm " + name + " and I belong to player " + data.name);
    }




    void OnEnable()
    {
        // Listening to other players death.
        OnDeath += OnSomebodyDied;
    }

    void OnDisable()
    {
        // Not caring anymore about other players death.
        OnDeath -= OnSomebodyDied;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Killer")
        {
            // Tell other players I died.
            if(OnDeath!=null)
                OnDeath(this);

            if(OnRoundOver != null)
                OnRoundOver();
        }
		else if(other.tag == "Finish")
        {
            // If I reached the finish I up my score.
            Winning();

            if (OnRoundOver != null)
                OnRoundOver();
        }
    }


    void OnSomebodyDied(PlayerInGame deadPlayer)
    {
        // We up the score if someone else died.
        if (deadPlayer != this)
        {

            IncreaseScore();

            // Moving the body back to start position for the next round.
            if (data.score < GameManager.instance.numberOfRounds)
                GetComponent<Body>().ResetToStartPosition();
            // If the game is over, there is no need to move back to the start.
        }
    }


    void IncreaseScore()
    {
        data.score++;

        if (data.score >= GameManager.instance.numberOfRounds)
            Winning();

        // Tell the world I have a new score.
        else if (OnScoreUpdated != null)
            OnScoreUpdated(data);
    }

    
    void Winning()
    {
        OnGameOver(data);
    }
    

    
}


public class PlayerData
{
    public PlayerAbbr id;
    public string name;
    public int score;
    public Character character;
}

/*
 * An id used to identify players across and scenes, even if the player in game script gets destroyed.
 * Also used by the input manager for assigning axises to players.
 */
public enum PlayerAbbr { P1, P2, P3, P4 }