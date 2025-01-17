﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{

    //We essentially want to set the size to the amount of players that are playing. 
    //You can modify this part of the script to change size and input the appropriate Gameobjects a the start of the level. 
    //It's probably a good idea track camera using the head.
    public List<GameObject> playerlist = new List<GameObject>();

    //A float to store the highest players y value
    float highestPlayer;

    //Vectors used for cameras position
    Vector3 cameraStartPosition;

    public float speed;

    bool isResettingToStart = false;
    public GameObject DeathZone;

    private void Awake()
    {
        //Storing the starting camera position
        cameraStartPosition = transform.position;
    }

    void OnEnable()
    {
        // Subscribing to round over events.
        PlayerInGame.OnRoundOver += CameraReset;
    }

    void OnDisable()
    {
        PlayerInGame.OnRoundOver -= CameraReset;
    }

    void Update()
    {
        //Storing the the central position for the players
        float playerCentre = 0;

        if(playerlist[0].transform.position.y > playerlist[1].transform.position.y)
        {
            highestPlayer = playerlist[0].transform.position.y;
        }else if (playerlist[1].transform.position.y > playerlist[0].transform.position.y)
        {
            highestPlayer = playerlist[1].transform.position.y;
        }

        playerCentre = highestPlayer;

        Vector3 target = new Vector3(transform.position.x, playerCentre, transform.position.z);

        //Moving the cameras position to the central, if not currently resetting.   
        if (playerCentre > transform.position.y && !isResettingToStart)
        {
            Debug.Log((target - transform.position) * Time.deltaTime * speed);
            transform.position += (target - transform.position) * Time.deltaTime * speed;
        }
    }

    //called to reset the cameras position at the bottom
    void CameraReset()
    {
        StartCoroutine(MoveToStart());
    }

    IEnumerator MoveToStart()
    {
        isResettingToStart = true;
        DeathZone.SetActive(false);
        float t = 0;
        Vector3 lerpBegin = transform.position;
        while (t < 1)
        {
            t += Time.deltaTime * speed;

            transform.position = Vector3.Lerp(lerpBegin, cameraStartPosition, t);

            yield return null;
        }
        transform.position = cameraStartPosition;
        DeathZone.SetActive(true);
        isResettingToStart = false;
    }
}