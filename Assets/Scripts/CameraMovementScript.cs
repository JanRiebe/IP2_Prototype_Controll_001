using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementScript : MonoBehaviour {

    //We essentially want to set the size to the amount of players that are playing. 
    //You can modify this part of the script to change size and input the appropriate Gameobjects a the start of the level. 
    //It's probably a good idea track camera using the head.
    public List<GameObject> playerlist = new List<GameObject>();
    List<float> positions = new List<float>();
    
    //Float number used to store central position of players
    float playerCentre;
    //the adjusted amount the camera moves up by
    public float heightAdjustment;

    //Vectors used for cameras position
    Vector3 cameraStartPosition;
    Vector3 currentCameraPosition;
    
    private void Awake()
    {
        //Storing the starting camera position
        cameraStartPosition = transform.position;
    }
	
	void Update ()
    {
        //Taking in the current camera position
         currentCameraPosition = transform.position;

        for (int i = 0; i < playerlist.Count; i++)
        {
            //Storing the the central position for the players
            playerCentre = playerCentre + playerlist[i].transform.position.y;
            positions.Add(playerlist[i].transform.position.y);
        }
        
        playerCentre = (playerCentre / playerlist.Count) + heightAdjustment;

        //Storing the central position of players
        Vector3 playerCentreV = new Vector3(transform.position.x, playerCentre, transform.position.z);

        //Moving the cameras position to the central 
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            if(playerCentreV.y > currentCameraPosition.y)
            {
                transform.position = currentCameraPosition + (playerCentreV - currentCameraPosition) * t;
            }
        }
    }
    
    //called to reset the cameras position at the bottom
    void CameraReset()
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            transform.position = currentCameraPosition - (currentCameraPosition - cameraStartPosition) * t;
        }

    }

}
