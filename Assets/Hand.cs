using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DistanceJoint2D))]
[RequireComponent(typeof(TargetJoint2D))]
public class Hand : MovableLimb
{
	[Tooltip("How long this hand will keep holding on to the handle, after the player has taken control over it.")]
    public float delayBeforeLettingGo;
	bool willSwitchToIK;	// Used to determine whether the hand still wants to switch over to IK. True while controlled, false when not controlled.

    // Indicates whether the limb could currently hold on to a handle.
    bool overHandle;
	
	DistanceJoint2D distJoint;
	TargetJoint2D targetJoint;

	public Transform shoulder;
	float maxDistanceToShoulder;

	bool contr = false;

	bool holdingOn = true;

	bool reachedTop;

	void Update()
	{
		if(contr)
		{
			GetComponent<SpriteRenderer>().color = Color.red;
		}
		else
			GetComponent<SpriteRenderer>().color = Color.white;
	}


	override protected void Initialise()
	{
		base.Initialise();
		distJoint = GetComponent<DistanceJoint2D>();
		targetJoint = GetComponent<TargetJoint2D>();
        parent = distJoint.connectedBody.GetComponent<Limb>();

		maxDistanceToShoulder = CalculateDistanceToShoulder();
	}


	private void OnTriggerEnter2D(Collider2D collision)
    {
		Debug.Log("OnTriggerEnter2D "+collision.name);
        if(collision.tag == "Handle" && reachedTop)
        {
            overHandle = true;
			SwitchToIK(this);
			holdingOn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Handle")
        {
            overHandle = false;
        }
    }

	// On button pressed.
	public void ActivateControl()
	{
		contr = true;
		//StartCoroutine(SwitchToFKAfterTime(delayBeforeLettingGo));
	}

	// On button released.
	public void DeactivateControl()
	{
		contr = false;
	}


	/// <summary>
    /// Call this to set this limb to be or not be controlled by the player.
    /// Causes the limb to switch between FK and IK.
    /// </summary>
    /// <param name="controlled">Whether the player controlls this limb.</param>
    override protected void SetControlled(bool controlled)
    {
	/*
		base.SetControlled(controlled);
		contr = controlled;

		willSwitchToIK = controlled;

        if (controlled)
        {
            StartCoroutine(SwitchToFKAfterTime(delayBeforeLettingGo));
        }
        else
        {
		//if (overHandle)
        //        SwitchToIK(this);
        }
		*/
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
		willSwitchToIK = false;
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

	public void Fire(float angle)
	{
		//if(holdingOn)
		//	return;

		Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
		StartCoroutine(TargetMover(dir, 1.5f));
		//SetControlled(false);
		holdingOn = false;
	}

	IEnumerator TargetMover(Vector2 direction, float speed)
	{
		while (maxDistanceToShoulder > CalculateDistanceToShoulder())
		{
			targetJoint.target += direction*speed*Time.deltaTime;
			yield return null;
		}
		SwitchToFK(this);
		reachedTop = true;
	}

	float CalculateDistanceToShoulder()
	{
		return Vector3.Distance(shoulder.position, transform.position);
	}
}
