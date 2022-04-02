using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    bool isOpen = false;
    bool isMoving = false;
    float openSpeed = 10;
    float delta = 5;
    float currDelta = 0;
    float inc;
    Vector3 closedPos;
    void Start()
    {
        inc  = openSpeed * delta / 1000;
        closedPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void toggleOpen(){
        if (!isMoving){
            isOpen = !isOpen;
            delta *= -1;
            currDelta = 0;
            StartCoroutine(toggleDoor());
        }
    }
    IEnumerator toggleDoor(){
        //Prevents multiple instances
        if (isMoving){
            yield break;
        }
        while(Mathf.Abs(currDelta) < Mathf.Abs(delta)){
            Vector3 currPos = transform.position;
            if(isOpen){
                currPos.y += inc;
                currDelta += inc;
            } else {
                currPos.y -= inc;
                currDelta -= inc;
            }
            transform.position = currPos;
            yield return null;
        }
        yield break;
    }
}
