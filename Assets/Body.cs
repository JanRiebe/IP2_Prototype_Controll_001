using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MovableLimb
{

	DistanceJoint2D[] distJoints;
    
	override protected void Initialise()
	{
		base.Initialise();
		distJoints = GetComponents<DistanceJoint2D>();
	}

    override public void SwitchToIK(Limb sender)
	{
		DistanceJoint2D connected = FindJointConnectedToSender(sender);
		if(connected != null)
			connected.enabled = true;
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
}
