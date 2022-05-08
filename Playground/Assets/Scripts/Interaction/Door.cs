using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    bool isOpen = false;
    bool isMoving = false;
    float openSpeed = 1;
    float moveDir = 1;
    float moveDist = 4;
    float openAmount;

    Vector3 closedPos;
    Vector3 closedScale;
    void Start()
    {
        closedPos = transform.localPosition;
        closedScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ToggleOpen()
    {
        isOpen = !isOpen;
        moveDir = isOpen ? 1 : -1;
        StartCoroutine(ToggleDoor());
    }
    public void Open()
    {
        isOpen = true;
        moveDir = isOpen ? 1 : -1;
        StartCoroutine(ToggleDoor());
    }
    public void Close()
    {
        isOpen = false;
        moveDir = isOpen ? 1 : -1;
        StartCoroutine(ToggleDoor());
    }
    IEnumerator ToggleDoor(){
        //Prevents multiple instances
        if (isMoving){
            yield break;
        }
        gameObject.GetComponent<AudioSource>().Play();
        while(openAmount >= 0 && openAmount <= 0.95){

            openAmount += openSpeed * moveDir * Time.deltaTime;

            transform.localPosition = closedPos + Vector3.up * openAmount * moveDist;
            transform.localScale = Vector3.Scale(closedScale, new Vector3(1, 1, 1 - openAmount));
            yield return null;
        }
        openAmount = Mathf.Clamp(openAmount, 0, 0.95f);
        transform.localPosition = closedPos + Vector3.up * openAmount * moveDist;
        transform.localScale = Vector3.Scale(closedScale, new Vector3(1, 1, 1 - openAmount));
        yield break;
    }
}
