using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(Rigidbody2D))]
public class ScreamWhenFalling : MonoBehaviour
{
	public AudioClip[] screamSounds;

	AudioSource audioSource;

	Rigidbody2D rb;
	public float screamAtVelocity;

	bool isScreaming;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.y <= -screamAtVelocity && !isScreaming)
			StartCoroutine(PlaySound());
    }


	IEnumerator PlaySound()
	{
		isScreaming = true;
		audioSource.clip = screamSounds[Random.Range(0,screamSounds.Length)];
		audioSource.Play();
		yield return new WaitForSeconds(audioSource.clip.length);
		isScreaming = false;
	}
}
