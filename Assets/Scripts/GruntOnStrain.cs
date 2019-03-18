using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntOnStrain : MonoBehaviour
{
    public AudioClip[] gruntSounds;

	public float strainLevel;

	AudioSource audioSource;


	bool isGrunting;


	DistanceJoint2D distJoint;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
		distJoint = GetComponent<DistanceJoint2D>();
    }


	void Update()
	{
		if(CalculateStrain() >= strainLevel && !isGrunting)
			StartCoroutine(PlaySound());
	}


	float CalculateStrain()
	{
		return distJoint.reactionForce.magnitude;
	}
	
	IEnumerator PlaySound()
	{
		isGrunting = true;
		audioSource.clip = gruntSounds[Random.Range(0,gruntSounds.Length)];
		audioSource.Play();
		yield return new WaitForSeconds(audioSource.clip.length);
		isGrunting = false;
	}
}
