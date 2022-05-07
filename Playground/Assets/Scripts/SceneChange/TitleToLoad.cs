using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class TitleToLoad : MonoBehaviour
{
    public GameObject loadscreen;
    public void onClick()
    {
        string path = Application.persistentDataPath;
        string[] files = Directory.GetFiles(path);
        string[] directories = Directory.GetDirectories(path);
        if (Array.IndexOf(directories, path + "\\Saves") == -1){
            Directory.CreateDirectory(path + "/Saves");
        }
        transform.parent.gameObject.SetActive(false);
        loadscreen.SetActive(true);

    }
}
