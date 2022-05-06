using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public SceneTransfer sceneTransfer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void onClick(){
        sceneTransfer.preLoad();
        SceneManager.LoadScene(sceneTransfer.sceneIndex);

    }

}
