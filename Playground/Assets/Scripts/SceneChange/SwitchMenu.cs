using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject menu;
    public void onClick()
    {
        transform.parent.gameObject.SetActive(false);
        menu.SetActive(true);
    }
    
}
