using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovableLimb: MonoBehaviour
{

	
	void Start()
	{
		Initialise();
	}


	[SerializeField]
    float strength;

	protected MovableLimb parent;

    bool isControlled;

    [SerializeField]
    Color controlledCollor = Color.red;
    Color basicColor;

    protected Rigidbody2D rb;
	

	GruntOnDemand gruntOnDemand;

    Vector3 startPosition;
    Quaternion startRotation;



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
            GetComponent<SpriteRenderer>().color = controlledCollor;
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
        if(isControlled)
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


}
