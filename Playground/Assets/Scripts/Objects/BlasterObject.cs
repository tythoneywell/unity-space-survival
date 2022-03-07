using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterObject : StorableObject {
    public BlasterObject() : base(new ItemStack(new BlasterItem(),1))
    {

    }
        // Start is called before the first frame update
    public override void onUse(GameObject gameObject){
        Debug.Log("pew pew");
    }
}
