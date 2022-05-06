using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildButton : MonoBehaviour
{
    public void Press()
    {
        PlayerUIController.main.OpenBuildMenu();
    }
}
