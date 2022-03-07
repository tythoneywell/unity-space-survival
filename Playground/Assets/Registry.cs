using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Registry : MonoBehaviour
{
    [SerializeField]
    GameObject blasterRef;
    [SerializeField]
    GameObject emptyRef;
    [SerializeField]
    GameObject scrapMetalRef;

    Dictionary<string, GameObject> registry= new Dictionary<string, GameObject>();
    void Start()
    {   
        registry["blaster"] = blasterRef;
        registry["empty"] = emptyRef;
        registry["scrap_metal"] = scrapMetalRef;
    }
    public GameObject get(string key){
        try{
            return registry[key];
        } catch (KeyNotFoundException){
            throw new System.Exception("Item is not in Registry");
        }
    }
    
}
