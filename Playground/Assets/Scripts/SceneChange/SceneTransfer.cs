using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class SceneTransfer : MonoBehaviour
{
    // Start is called before the first frame update
    public int sceneIndex;
    public abstract void preLoad();
}
