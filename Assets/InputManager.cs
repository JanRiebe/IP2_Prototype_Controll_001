using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public PairInput[] pairInput;

    [System.Serializable]
    public class PairInput
    {
        [Tooltip("The button that will be used to control the limb")]
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
            
            if (Input.GetButtonDown(pair.input))
            {
                playerControlingInput(true, pair.limb);
                
                activeLimbs++;
            }

            if(Input.GetButtonUp(pair.input))
            {
                playerControlingInput(false, pair.limb);

                activeLimbs--;
            }
        }

        foreach (PairAxis pair in pairAxis)
        {

            float horizontalAxis = Input.GetAxis(pair.axis[0]);
            float verticalAxis = Input.GetAxis(pair.axis[1]);

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
