using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class TitleToLoad : SceneTransfer
{

    public TitleToLoad(){
        sceneIndex = 1;
    }
    public override void preLoad()
    {
        string path = Application.persistentDataPath;
        string[] files = Directory.GetFiles(path);
        string[] directories = Directory.GetDirectories(path);
        if (Array.IndexOf(directories, path + "\\Saves") == -1){
            Directory.CreateDirectory(path + "/Saves");
        }
    }
}
