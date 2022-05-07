using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitToDesktop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onClick(){
        #if UNITY_STANDALONE_WIN
            Application.Quit();
        #endif
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
