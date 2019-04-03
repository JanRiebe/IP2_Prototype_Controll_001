using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePauser : MonoBehaviour
{
    public delegate void PauseMessage(bool paused);
    public static event PauseMessage OnGamePaused;

    bool isGamePaused;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause") && OnGamePaused != null)
        {
            Pause(!isGamePaused);
        }
    }

    
    public void Pause(bool pause)
    {
        isGamePaused = pause;
        OnGamePaused(isGamePaused);
        Time.timeScale = isGamePaused ? 0 : 1;
    }
}
