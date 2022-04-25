using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "ScriptableObjects/Recipe", order = 1)]
public class Recipe : ScriptableObject
{
    // Start is called before the first frame update
    public ItemStack[] requirements;
    public ItemStack result;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
