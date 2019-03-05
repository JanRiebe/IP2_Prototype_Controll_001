using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MovableLimb
{

	DistanceJoint2D[] distJoints;
	TargetJoint2D targetJoint;
    
	override protected void Initialise()
	{
		base.Initialise();
		distJoints = GetComponents<DistanceJoint2D>();
		targetJoint = GetComponent<TargetJoint2D>();
	}


	override public void SetControlled(bool controlled)
    {
		base.SetControlled(controlled);
				
		// When the player stops controlling the body, it will hold its last position.
		// When the body is set controlled it will stop holding its position.
		HoldPosition(!controlled);
    }


    override public void SwitchToIK(Limb sender)
	{
		DistanceJoint2D connected = FindJointConnectedToSender(sender);
		if(connected != null)
			connected.enabled = true;
		// If any of the other limbs take controll, the body stops holing on to it's position.
		HoldPosition(false);
	}



    override public void SwitchToFK(Limb sender)
	{
		DistanceJoint2D connected = FindJointConnectedToSender(sender);
		if(connected != null)
			connected.enabled = false;
	}



	DistanceJoint2D FindJointConnectedToSender(Limb sender)
	{
		foreach(DistanceJoint2D j in distJoints)
		{
			if(j.connectedBody == sender.GetComponent<Rigidbody2D>())
			return j;
		}
		return null;
	}


	void HoldPosition(bool hold)
	{
		targetJoint.target = transform.position;
		targetJoint.enabled = hold;
	}
}
