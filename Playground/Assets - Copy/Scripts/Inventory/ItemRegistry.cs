using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRegistry : MonoBehaviour
{
    [SerializeField]
    ItemData[] itemList;

    Dictionary<string, ItemData> registry = new Dictionary<string, ItemData>();
    void Awake()
    {
        foreach (ItemData item in itemList)
        {
            registry.Add(item.itemName, item);
        }
    }

    public ItemData get(string key){
        try{
            return registry[key];
        } catch (KeyNotFoundException){
            throw new System.Exception("Item is not in Registry");
        }
    }
    
}
