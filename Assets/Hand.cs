using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HingeJoint2D), typeof(HingeJoint2D))]
public class Hand : MovableLimb
{
	// The handle that this hand is currently over, null if not over any handle.
	public Rigidbody2D _currentHandle;

	[SerializeField]
	MovableLimb _parentLimb;

	// Connections to other limbs.
    HingeJoint2D _connectionToArm;
    HingeJoint2D _connectionToHandhold;
	   
    override protected void Initialise()
	{
		base.Initialise();

		// Assigning hinge joint variables.
        HingeJoint2D[] joints = GetComponents<HingeJoint2D>();
		_connectionToArm = joints[0];
		_connectionToHandhold = joints[1];
		_connectionToArm.connectedBody = _parentLimb.GetComponent<Rigidbody2D>();
		_connectionToHandhold.connectedBody = null;
		_connectionToArm.enabled = true;
		_connectionToHandhold.enabled = false;

		// Initialise parent limb with arm.
        parent = _connectionToArm.connectedBody.GetComponent<MovableLimb>();

	}


	private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Handle" || 
		(other.tag == "Player" && other.GetComponent<MovableLimb>().WhichBodyDoYouBelongTo() != WhichBodyDoYouBelongTo()))
        {
            _currentHandle = other.GetComponent<Rigidbody2D>();
			if(!isControlled)
				SwitchToIK(this);		
				
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Handle" || 
		(other.tag == "Player" && other.GetComponent<MovableLimb>().WhichBodyDoYouBelongTo() != WhichBodyDoYouBelongTo()))
        {
            _currentHandle = null;
        }
    }



	/// <summary>
    /// Call this to set this limb to be or not be controlled by the player.
    /// Causes the limb to switch between FK and IK.
    /// </summary>
    /// <param name="controlled">Whether the player controlls this limb.</param>
    override public void SetControlled(bool controlled)
    {
		base.SetControlled(controlled);
        
        if (controlled)
			SwitchToFK(this);		
        else if (_currentHandle)
            SwitchToIK(this);
    }

	
    override protected void SwitchToIK(MovableLimb sender)
	{
		// Switch on the joint for connecting to handholds.
		_connectionToHandhold.enabled = true;
		// Assign the handhold as connected body.
		_connectionToHandhold.connectedBody = _currentHandle;
		// Switch off the joint connecting to the arm.
		_connectionToArm.enabled = false;

		base.SwitchToIK(this);
	}

    override protected void SwitchToFK(MovableLimb sender)
	{	
		// Switch off the joint for connecting to handholds.
		_connectionToHandhold.enabled = false;
		// Assign null as connected body.
		_connectionToHandhold.connectedBody = null;
		// Switch on the joint connecting to the arm.
		_connectionToArm.enabled = true;

		base.SwitchToFK(this);
	}

}
