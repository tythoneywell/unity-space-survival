using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorableObject : InteractableObject
{
    ItemStack itemStack;
    public StorableObject(ItemStack itemStack){
        this.itemStack = itemStack;
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
        bool res = player.AddToInventory(this.itemStack);
        if(res){
            Destroy(gameObject);
        }
        Debug.Log("WasAddedToInv: " + res);
    }
    public virtual void onUse(GameObject presser){
        
    }
}
