using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovableLimb: MonoBehaviour
{

	
	void Start()
	{
		Initialise();
	}

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
	
	[SerializeField]
    float strength;

	protected MovableLimb parent;

    protected Vector3 startPosition;
    protected Quaternion startRotation;



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

    protected Rigidbody2D rb;
	
	bool wasControlledInPreviousUpdate = false;		//TODO find more elegant solution


	GruntOnDemand gruntOnDemand;





    virtual protected void Initialise()
    {
        // Saving the world space position and rotation at level start, to use them later in ResetToStartPosition.
        startPosition = transform.position;
        startRotation = transform.rotation;

        rb = GetComponent<Rigidbody2D>();
		gruntOnDemand = GetComponent<GruntOnDemand>();
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
		if(isControlledByPlayer)
		{
        if (Input.GetKey(KeyCode.UpArrow))
            ForceDirection(Vector2.up);
        if (Input.GetKey(KeyCode.DownArrow))
            ForceDirection(Vector2.down);
        if (Input.GetKey(KeyCode.LeftArrow))
            ForceDirection(Vector2.left);
        if (Input.GetKey(KeyCode.RightArrow))
            ForceDirection(Vector2.right);
			}
			/*
        if (Input.GetKey(KeyCode.Q))
            ForceRotation(true);
        if (Input.GetKey(KeyCode.E))
            ForceRotation(false);
			*/
    }



	

    public virtual void SetControlled(bool controlled)
	{
		if(controlled)
		{
			timeOfTakingControl = Time.time;
			//gruntOnDemand.PleaseGruntNow();
		}
	}

    /// <summary>
    /// Call this to apply a force in a global direction, independently of the limbs own orientation.
    /// </summary>
    /// <param name="direction">A Vector2 representing the direction to move in. 
    /// The magnitude of the vector does not affect the amount of foce.
    /// Should not be of magnitude 0.</param>
    public void ForceDirection(Vector2 direction)
    {
        rb.AddForce(direction.normalized * strength * forceFactor/* * AttackRightNow()*/);
		if(gruntOnDemand)
			gruntOnDemand.PleaseGruntNow();
    }

    

	float AttackRightNow()
	{
		float timePassed = Time.time - timeOfTakingControl;
		float progress = Mathf.Min(1.0f, timePassed/attack.attackTime);
		return attack.attackOverTime.Evaluate(progress);
	}


	virtual protected void SwitchToIK(MovableLimb sender)
	{
		parent.SwitchToIK(this);
	}

    virtual protected void SwitchToFK(MovableLimb sender)
	{	
		parent.SwitchToFK(this);
	}

	// Returns the body that this limb is connected to, even if there are other limbs in between.
	virtual public Body WhichBodyDoYouBelongTo()
	{
		return parent.WhichBodyDoYouBelongTo();
	}


    abstract public void ResetToStartPosition();

}
