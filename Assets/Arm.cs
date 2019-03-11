using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HingeJoint2D))]
public class Arm : MovableLimb
{
	[SerializeField]
	MovableLimb _parentLimb, _childLimb;

	HingeJoint2D _connectionToParent;
	HingeJoint2D _connectionToChild;

    override protected void Initialise()
	{
		base.Initialise();

		parent = _parentLimb;

		HingeJoint2D[] hinges = GetComponents<HingeJoint2D>();
		_connectionToParent = hinges[0];
		_connectionToChild = hinges[1];
		_connectionToParent.connectedBody = _parentLimb.GetComponent<Rigidbody2D>();
		_connectionToChild.connectedBody = _childLimb.GetComponent<Rigidbody2D>();
		_connectionToParent.enabled = true;
		_connectionToChild.enabled = false;
	}

    override protected void SwitchToIK(MovableLimb sender)
	{
		_connectionToParent.enabled = false;
		_connectionToChild.enabled = true;

		base.SwitchToIK(this);
	}

    override protected void SwitchToFK(MovableLimb sender)
    {
		_connectionToParent.enabled = true;
		_connectionToChild.enabled = false;

        base.SwitchToIK(this);
	}
    /*

    override protected bool IsAnyLimbHoldingOn()
    {
        //TODO
        return parent.IsAnyLimbHoldingOn();
    } 
    */

}
