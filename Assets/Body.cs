using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MovableLimb
{

	HingeJoint2D[] hinges;
	TargetJoint2D targetJoint;
    
	override protected void Initialise()
	{
		base.Initialise();
		hinges = GetComponents<HingeJoint2D>();
		targetJoint = GetComponent<TargetJoint2D>();
	}


	override public void SetControlled(bool controlled)
    {
		base.SetControlled(controlled);
				
		// When the player stops controlling the body, it will hold its last position.
		// When the body is set controlled it will stop holding its position.
		HoldPosition(!controlled && IsSomeoneIK());
    }


    override protected void SwitchToIK(MovableLimb sender)
	{
		
		HingeJoint2D connected = FindJointConnectedToSender(sender);
		if(connected != null)
			connected.enabled = true;
		// If any of the other limbs take controll, the body stops holing on to it's position.
		HoldPosition(false);
	}



    override protected void SwitchToFK(MovableLimb sender)
	{
		HingeJoint2D connected = FindJointConnectedToSender(sender);
		if(connected != null)
			connected.enabled = false;
	}



	HingeJoint2D FindJointConnectedToSender(MovableLimb sender)
	{
		foreach(HingeJoint2D j in hinges)
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

	override public Body WhichBodyDoYouBelongTo()
	{
		return this;
	}
    /*
    protected override bool IsAnyLimbHoldingOn()
    {
        // Checking through all hinges, whether they are active.
        foreach(HingeJoint2D h in hinges)
        {
            if (h.isActiveAndEnabled)
            {
                // If they are active, it means they are IK.
                // So we need to check whether they are also controlled.
                // If they are not controlled it means they are holding on.
                return true;
            }
        }
        // If no joint is active, it means all limbs are FK, in which case we return false;
        return false;
    }
    */
}
