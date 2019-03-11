using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementScript : MonoBehaviour {


    //We essentially want to set the size to the amount of players that are playing. 
    //You can modify this part of the script to change size and input the appropriate Gameobjects a the start of the level. 
    //It's probably a good idea track camera using the head.
    public List<GameObject> playerlist = new List<GameObject>();
    List<float> positions = new List<float>();
    float top;
	
    //SideNote: Camera goes up, but doesn't go down. It's debateble whether this should be fixed.

	void Start () {
		
	}
	
	
	void Update () {
        
        
        for (int i = 0; i < playerlist.Count; i++)
        {
            positions.Add(playerlist[i].transform.position.y);
        }
        top = Max(positions);
        //the - modifier with top lets us give the player just enough space to grab stuff above them, but not so much that they are instantly knocking opponents out.
        transform.position = new Vector3(transform.position.x, top - 2.5f, transform.position.z);
        
	}

    //I had to make a quick function that checks for the highest. I found that setting Max to 4 allows for smoother transitions
    // from the start of the level to going up inaction.
    float Max(List<float> list)
    {
        float max = 4;
        for(int i = 1; i < list.Count; i++)
        {
            if(list[i] > max)
            {
                max = list[i];
            }
        }

        return max;
    }
}
