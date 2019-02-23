using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HingeJoint2D))]
[RequireComponent(typeof(TargetJoint2D))]
public class Hand : MovableLimb
{
	[Tooltip("How long this hand will keep holding on to the handle, after the player has taken control over it.")]
    public float delayBeforeLettingGo;
	bool willSwitchToIK;	// Used to determine whether the hand still wants to switch over to IK. True while controlled, false when not controlled.

    // Indicates whether the limb could currently hold on to a handle.
    bool overHandle;
	
	HingeJoint2D distJoint;
	TargetJoint2D targetJoint;


	override protected void Initialise()
	{
		base.Initialise();
		distJoint = GetComponent<HingeJoint2D>();
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
    override protected void SetControlled(bool controlled)
    {
	base.SetControlled(controlled);

	willSwitchToIK = controlled;

        if (controlled)
        {
            StartCoroutine(SwitchToFKAfterTime(delayBeforeLettingGo));
        }
        else
        {
		if (overHandle)
                SwitchToIK(this);
        }
    }

	IEnumerator SwitchToFKAfterTime(float time)
	{
		float startTime = Time.time;
		while(Time.time < startTime + time)
		{
			if(!willSwitchToIK)
				break;
			yield return null;	
		}
		if(willSwitchToIK)
			SwitchToFK(this);
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
