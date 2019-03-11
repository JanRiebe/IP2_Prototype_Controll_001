/*
 * The purpose of this script is to handle the death of a body. 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerInGame : MonoBehaviour
{

    public string playerName;

    int score = 0;

    delegate void TellThemWhoDidIt(PlayerInGame sender);
	static event TellThemWhoDidIt OnDeath;

    public delegate void TellScore(PlayerInGame player, int score);
    public static event TellScore OnScoreUpdated;

    Vector2 startPosition;

    
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


    private void Start()
    {
        // Storing the start position so we can be moved there when the level ends.
        startPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Killer")
        {
            // Tell other players I died.
            OnDeath(this);

            OnRoundOver();
        }
		else if(other.tag == "Finish")
        {
            // If I reached the finish I up my score.
            IncreaseScore();

            OnRoundOver();
        }
    }


    void OnSomebodyDied(PlayerInGame deadPlayer)
    {
        // We up the score if someone else died.
        if (deadPlayer != this)
        {
            IncreaseScore();
            OnRoundOver();
        }
    }


    void IncreaseScore()
    {
        score++;
        // Tell the world I have a new score.
        OnScoreUpdated(this, score);
    }


    void OnRoundOver()
    {
        // Moving the body back to start position for the next round.
        if (score < GameManager.instance.numberOfRounds)
            GetComponent<Body>().ResetToStartPosition();
        // If the game is over, there is no need to move back to the start.
    }
    

    
}
