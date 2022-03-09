using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Random.rotation;
        transform.localScale *= (Random.value + 1) / 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
