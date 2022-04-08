using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGame : SceneTransfer
{
    string saveName;
    LoadGame(string saveName){
        sceneIndex = 2;
        this.saveName = saveName;
    }
    public override void preLoad()
    {
        Debug.Log("Load Game Data");
    }
}
