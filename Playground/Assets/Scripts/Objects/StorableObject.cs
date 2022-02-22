using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorableObject : BaseObject
{
    Item item;
    StorableObject(Item item){
        this.item = item;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    public override void onInteract(GameObject presser){
        Player player = (Player)presser.GetComponent("Player");
        bool res = player.addToInventory(new ItemStack(new ScrapMetalItem(), 10));
        Debug.Log(res);
    }
}
