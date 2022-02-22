using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    Texture texture;
    public string regName;
    public int maxStackSize;
    public Item(Texture texture, string name, int maxStack){
        this.texture = texture;
        this.regName = name;
        this.maxStackSize = maxStack;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static Item empty(){
        return new Item(null, "empty", 0);
    }
}
