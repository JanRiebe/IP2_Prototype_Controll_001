// Assets
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovableLimb: MonoBehaviour
{

	




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

    
    [SerializeField]
    float strength;

    float timeOfTakingControl;


    protected MovableLimb parent;

    // Indicates whether this limb is currently controlled.
    protected bool isControlled;

    [SerializeField]
    Color controlledCollor = Color.red;
    Color basicColor;

    protected Rigidbody2D rb;
	

	GruntOnDemand gruntOnDemand;

    protected Vector3 startPosition;
    protected Quaternion startRotation;


    void Start()
    {
        Initialise();
    }


    void OnEnable()
    {
        PlayerInGame.OnRoundOver += ResetToStartPosition;
    }

    void OnDisable()
    {
        PlayerInGame.OnRoundOver -= ResetToStartPosition;
    }



    virtual protected void Initialise()
    {

        // Saving the world space position and rotation at level start, to use them later in ResetToStartPosition.
        startPosition = transform.position;
        startRotation = transform.rotation;

        basicColor = GetComponent<SpriteRenderer>().color;

        rb = GetComponent<Rigidbody2D>();
		gruntOnDemand = GetComponent<GruntOnDemand>();
    }


	

    public virtual void SetControlled(bool controlled)
	{
        if (controlled)
        {
            timeOfTakingControl = Time.time;
            GetComponent<SpriteRenderer>().color = controlledCollor;
        }
        else
            GetComponent<SpriteRenderer>().color = basicColor;

        isControlled = controlled;
    }

    /// <summary>
    /// Call this to apply a force in a global direction, independently of the limbs own orientation.
    /// </summary>
    /// <param name="direction">A Vector2 representing the direction to move in. 
    /// The magnitude of the vector does not affect the amount of foce.
    /// Should not be of magnitude 0.</param>
    public void ForceDirection(Vector2 direction)
    {
        if (isControlled)// && IsAnyLimbHoldingOn())
            rb.AddForce(direction.normalized * strength);

		if(gruntOnDemand)
			gruntOnDemand.PleaseGruntNow();
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

    float AttackRightNow()
    {
        float timePassed = Time.time - timeOfTakingControl;
        float progress = Mathf.Min(1.0f, timePassed / attack.attackTime);
        return attack.attackOverTime.Evaluate(progress);
    }


    public virtual void ResetToStartPosition()
    {
        SetControlled(false);
        transform.position = startPosition;
        transform.rotation = startRotation;
        rb.velocity = Vector3.zero;
    }
}
