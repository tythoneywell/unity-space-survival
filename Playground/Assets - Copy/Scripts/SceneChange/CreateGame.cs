using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class CreateGame : SceneChanger
{
    // Start is called before the first frame update
    public override void onClick(){
        string playerName = GameObject.Find("PlayerName").GetComponentInChildren<Text>().text;
        string saveName = GameObject.Find("SaveName").GetComponentInChildren<Text>().text;
        string savesPath = Application.persistentDataPath + "\\Saves";
        string[] saves = Directory.GetDirectories(savesPath);
        for(int i = 0; i < saves.Length; i++){
            if (saves[i] == savesPath + "\\" + saveName){
                return;
            }
        }
        Directory.CreateDirectory(savesPath + "\\" + saveName);
        sceneTransfer.preLoad();
        SceneManager.LoadScene(sceneTransfer.sceneIndex);

    }
}
