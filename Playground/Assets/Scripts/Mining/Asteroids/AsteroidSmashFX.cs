using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSmashFX : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] clips;

    void Start()
    {
        audioSource.clip = clips[(int)Mathf.Clamp(Random.value * clips.Length, 0, clips.Length)];
        audioSource.Play();
    }

    void Update()
    {
        if (!audioSource.isPlaying) Destroy(gameObject);
    }
}
