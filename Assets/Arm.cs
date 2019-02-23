using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HingeJoint2D))]
public class Arm : MovableLimb
{

	//public Limb child;
    HingeJoint2D[] distJoints;
    //DistanceJoint2D childJoint;


    override protected void Initialise()
	{
		base.Initialise();
        distJoints = GetComponents<HingeJoint2D>();
	}

    override public void SwitchToIK(Limb sender)
	{
        SwitchDirection(true, sender);
		parent.SwitchToIK(this);
	}

    override public void SwitchToFK(Limb sender)
    {
        SwitchDirection(false, sender);
        parent.SwitchToIK(this);
	}

    void SwitchDirection(bool dangleFromSender, Limb sender)
    {
        foreach (HingeJoint2D j in distJoints)
        {
            if (j.connectedBody == sender.GetComponent<Rigidbody2D>())
                j.enabled = dangleFromSender;
            else
            {
                j.enabled = !dangleFromSender;
                parent = j.connectedBody.GetComponent<Limb>();
            }
        }
    }
}
