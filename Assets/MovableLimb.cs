using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovableLimb : Limb
{

	
	

    [System.Serializable]
    public class ActivationAxis
    {
        [Tooltip("The name of the axis that switches this limb on.")]
        public string name;
        [Tooltip("How much this limb is afected by direction input after it has been activated by this axis.")]
        [Range(0, 1)]
        public float influence = 1.0f;
    }
    
    public ActivationAxis[] activationInputs;
	
	
    public float strength;

	protected Limb parent;



	
    [System.Serializable]
    public class Attack
    {
        [Tooltip("The time in seconds it takes the limb to reach its final force strength.")]
        public float attackTime = 1;

        [Tooltip("The amount of force that will be applied over the duration of attackTime.")]
        public AnimationCurve attackOverTime;
    }

    [Tooltip("How forces act on the limb over time when the player makes an input.")]
    public Attack attack;
	

	float timeOfTakingControl;	// The time at which the limb last started to be controlled by the player.


    float forceFactor = 1;	// Determins if and how much a limb is currently controlled by directional input.

    Rigidbody2D rb;
	
	bool wasControlledInPreviousUpdate = true;		//TODO find more elegant solution

    override protected void Initialise()
    {
        rb = GetComponent<Rigidbody2D>();
    }

      // Update is called once per frame
    void Update()
    {
        // Calculating the force factor.
        forceFactor = 0.0f;
        bool isControlledByPlayer = false;
        foreach(ActivationAxis axis in activationInputs)
        {
            if (Input.GetAxis(axis.name) > 0.1f)
            {
                forceFactor += axis.influence;
                isControlledByPlayer = true;
            }
        }
        forceFactor = Mathf.Min(1.0f, forceFactor);

        // Setting this to forward or backward kinematic, depending on whether any of the controlling axes are active.
		if(wasControlledInPreviousUpdate != isControlledByPlayer)
		{
			SetControlled(isControlledByPlayer);
			wasControlledInPreviousUpdate = isControlledByPlayer;
		}
		
		if(isControlledByPlayer)
		{
			GetComponent<SpriteRenderer>().color = Color.red;
		}
		else
			GetComponent<SpriteRenderer>().color = Color.white;

        //TODO move these in input control script, that calls force functions
        if (Input.GetKey(KeyCode.UpArrow))
            ForceDirection(Vector2.up);
        if (Input.GetKey(KeyCode.DownArrow))
            ForceDirection(Vector2.down);
        if (Input.GetKey(KeyCode.LeftArrow))
            ForceDirection(Vector2.left);
        if (Input.GetKey(KeyCode.RightArrow))
            ForceDirection(Vector2.right);

        if (Input.GetKey(KeyCode.Q))
            ForceRotation(true);
        if (Input.GetKey(KeyCode.E))
            ForceRotation(false);
    }



	

    protected virtual void SetControlled(bool controlled)
	{
		if(controlled)
			timeOfTakingControl = Time.time;
	}

    /// <summary>
    /// Call this to apply a force in a global direction, independently of the limbs own orientation.
    /// </summary>
    /// <param name="direction">A Vector2 representing the direction to move in. 
    /// The magnitude of the vector does not affect the amount of foce.
    /// Should not be of magnitude 0.</param>
    public void ForceDirection(Vector2 direction)
    {
        rb.AddForce(direction.normalized * strength * forceFactor * AttackRightNow());
    }

    

    public void ForceRotation(bool clockwise)
    {
        // Applies a directional force, depending on the vector to the limb it is dangling from,
        // to create the illusion of rotation.
        if (parent == null)
            Debug.Log("Parent null on " + name);
        Vector2 limbDirection = transform.position - parent.transform.position;
        // The angle between this limb and the x-axis, -180 to 180
        float angle = Vector2.SignedAngle(Vector2.right, limbDirection);
        /* Angles in the coordinate system relative to parent limb.
         * Imagine this limb to rotate around P and be in on of the four quadrants.
         * 
         *  180 - 90  |  90 - 0
         * -----------P-----------x
         * -180 - -90 | -90 - 0
         * 
         * x = x-axis
         * P = parent limb
         */
         
        // Setting the direction to apply force in depending on the roation.
        Vector2 dir = Vector2.zero;
        if (angle > 90)
            dir = Vector2.right;
        else if (angle > 0)
            dir = Vector2.down;
        else if (angle < -90)
            dir = Vector2.up;
        else //if (angle < 0)
            dir = Vector2.left;

        // Reversing the direction, if we want to rotate counter clockwise.
        if (!clockwise)
            dir = -dir;
            
        ForceDirection(dir);
        
    }


	float AttackRightNow()
	{
		float timePassed = Time.time - timeOfTakingControl;
		float progress = Mathf.Min(1.0f, timePassed/attack.attackTime);
		return attack.attackOverTime.Evaluate(progress);
	}

	
}
