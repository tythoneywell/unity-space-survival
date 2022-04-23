using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : SceneTransfer
{
    // Start is called before the first frame update
    void Start()
    {
        sceneIndex = 0;

    }
    public override void preLoad()
    {
        Debug.Log("Load Game Data");
    }
}
