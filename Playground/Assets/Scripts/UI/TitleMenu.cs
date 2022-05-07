using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenu : MonoBehaviour
{
    // Start is called before the first frame update

    void Update()
    {
        gameObject.transform.RotateAround(new Vector3(0,0,0), new Vector3(0, 1, 0), 5 * Time.deltaTime);
    }
}
