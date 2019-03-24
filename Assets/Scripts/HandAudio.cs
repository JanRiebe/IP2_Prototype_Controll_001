using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HandAudio : MonoBehaviour
{
    public AudioClip letGoSound;
    public AudioClip grabOnSound;
    AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayLetGoSound()
    {
        StartCoroutine(PlaySound(letGoSound));
    }

    public void PlayGrabOnSound()
    {
        StartCoroutine(PlaySound(grabOnSound));
    }

    IEnumerator PlaySound(AudioClip sound)
    {
        source.clip = sound;
        source.Play();
        yield return new WaitForSeconds(source.clip.length);
    }
}
