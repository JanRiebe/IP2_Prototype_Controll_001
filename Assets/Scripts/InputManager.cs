using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    
    

    public PlayerAbbr playerAbbreviation;

    public PairInput[] pairInput;


    [System.Serializable]
    public class PairInput
    {
        [Tooltip("The button that will be used to control the limb, without the player abbreviation.")]
        public string input;

        //[Tooltip("The limbs you would like to control")]
        public MovableLimb limb;
    }


    [System.Serializable]
    public class PairAxis
    {
        public string[] axis;

        //[Tooltip("The limbs you would like to control")]
        public MovableLimb[] limb;
        
    }

    public PairAxis[] pairAxis;
    
    float[] movementDirection;

    int activeLimbs = 0;
    public int maxLimbs = 4;
    bool maxLimbsB;


    
    // Update is called once per frame
    void Update()
    {
        if (activeLimbs == maxLimbs)
        {
            maxLimbsB = true;
        }
        else
            maxLimbsB = false;


        foreach (PairInput pair in pairInput)
        {
            
            if (Input.GetButtonDown(pair.input+playerAbbreviation.ToString()))
            {
                playerControlingInput(true, pair.limb);
                
                activeLimbs++;
            }

            if(Input.GetButtonUp(pair.input + playerAbbreviation.ToString()))
            {
                playerControlingInput(false, pair.limb);

                activeLimbs--;
            }
        }
        foreach (PairAxis pair in pairAxis)
        {
            float horizontalAxis = Input.GetAxis(pair.axis[0] + playerAbbreviation.ToString());
            float verticalAxis = Input.GetAxis(pair.axis[1] + playerAbbreviation.ToString());

            // Adding axises for keyboard input
            if (pair.axis.Length > 3)
            {
                horizontalAxis += Input.GetAxis(pair.axis[2] + playerAbbreviation.ToString());
                verticalAxis += Input.GetAxis(pair.axis[3] + playerAbbreviation.ToString());
            }

            Vector2 movement = new Vector2(horizontalAxis, verticalAxis);

            for (int i = 0; i < pair.limb.Length; i++)
            {
                playercontrollingAxis(movement, horizontalAxis, verticalAxis, pair.limb[i]);
            }
        }

        

    }

    public void playerControlingInput(bool controlled, MovableLimb limb)
    {
        if (activeLimbs < maxLimbs || !controlled)
        {
            limb.SetControlled(controlled);
        }   
    }

    public void playercontrollingAxis(Vector2 movement, float horizontalAxis, float verticalAxis, MovableLimb limb)
    {
        if (maxLimbsB == false)
        {
            limb.ForceDirection(movement);
        }
    }
}
