﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DistanceJoint2D))]
[RequireComponent(typeof(TargetJoint2D))]
public class Hand : MovableLimb
{
    
    // Indicates whether the limb could currently hold on to a handle.
    bool overHandle;
	
	DistanceJoint2D distJoint;
	TargetJoint2D targetJoint;


	override protected void Initialise()
	{
		base.Initialise();
		distJoint = GetComponent<DistanceJoint2D>();
		targetJoint = GetComponent<TargetJoint2D>();
        parent = distJoint.connectedBody.GetComponent<Limb>();
	}


	private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Handle")
        {
            overHandle = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Handle")
        {
            overHandle = false;
        }
    }



    /// <summary>
    /// Call this to set this limb to be or not be controlled by the player.
    /// Causes the limb to switch between FK and IK.
    /// </summary>
    /// <param name="controlled">Whether the player controlls this limb.</param>
    public virtual void SetControlled(bool controlled)
    {
		
        if (controlled)
        {
            SwitchToFK(this);
        }
        else
        {
		if (overHandle)
                SwitchToIK(this);
        }
    }


	
    override public void SwitchToIK(Limb sender)
	{
		distJoint.enabled = false;
		targetJoint.enabled = true;
		targetJoint.target = transform.position;
		parent.SwitchToIK(this);
	}

    override public void SwitchToFK(Limb sender)
	{	
		distJoint.enabled = true;
		targetJoint.enabled = false;
		parent.SwitchToFK(this);
	}
}
