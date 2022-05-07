using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 *  This class plays the outro dialogue which is contained in theOutro.
 *  It then plays the outro ambient sound loop. The sound is set to loop on its
 *  own.
 */

public class OutroAudioScript : MonoBehaviour
{

    bool outroLoopPlaying;
    public AudioSource theOutro;
    public AudioSource theOutroLoop;
    private Canvas credits;

    // Start is called before the first frame update
    void Start()
    {
        theOutro.Play();
        outroLoopPlaying = false;
        credits = GetComponentInChildren<Canvas>();
        credits.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!theOutro.isPlaying && !outroLoopPlaying)
        {
            theOutroLoop.Play();
            outroLoopPlaying = true;
        }

        if (!theOutro.isPlaying && outroLoopPlaying)
        {
            credits.enabled = true;
        }
    }
}
