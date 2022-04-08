using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Linq;


public class DropdownSelector : MonoBehaviour
{
    void Awake()
    {
        Dropdown dropdown = gameObject.GetComponent<Dropdown>();
        string path = Application.persistentDataPath;
        List<string> options = new List<string>();
        string[] saves = Directory.GetDirectories(path + "\\Saves");
        foreach(string save in saves){
            string[] parsed = save.Split('\\');
            options.Add(parsed[parsed.Length - 1]);
        }
        dropdown.ClearOptions();
        if (saves.Length == 0){
            options.Add("No Saves");
        }
        dropdown.AddOptions(options);
    }
}
