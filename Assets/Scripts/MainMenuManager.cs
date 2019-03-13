using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{

    public void OnPlayButtonPressed()
    {
        GameManager.instance.StartGame();
    }

}
