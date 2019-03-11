using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntOnCollision : MonoBehaviour
{
	public AudioClip[] gruntSounds;

	AudioSource audioSource;


	bool isGrunting;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }



	void OnCollisionEnter2D(Collision2D collision)
	{
		if(!isGrunting && collision.collider.tag == "Hand")
			StartCoroutine(PlaySound());
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
