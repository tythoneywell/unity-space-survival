using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroCutScene : MonoBehaviour
{

    public AudioSource theIntro;
    
    // Start is called before the first frame update
    void Start()
    {
        theIntro = GetComponent<AudioSource>();
        theIntro.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!theIntro.isPlaying)
        {
            SceneManager.LoadScene("main");
        }
    }
}
