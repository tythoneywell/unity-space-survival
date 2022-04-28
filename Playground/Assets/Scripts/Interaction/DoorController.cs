using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : InteractableObject
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnInteract(PlayerInteraction presser){
        GameObject door = GameObject.Find("DoorPlate");
        Door doorPlate = door.GetComponent<Door>();
        doorPlate.toggleOpen();
    }
    
}
