using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableObject
{
    // Start is called before the first frame update
    bool isOpen = false;
    float delta = 5;
    Vector3 closedPos;
    void Start()
    {
        closedPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void toggleOpen(){
        isOpen = !isOpen;
        Vector3 currPos = transform.position;
        if(isOpen){
            currPos.y += delta;
        } else {
            currPos.y -= delta;
        }
        transform.position = currPos;
    }
    public override void Interact(PlayerInteraction presser){
        toggleOpen();
    }
}
