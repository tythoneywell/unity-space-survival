using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinFast : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(Random.value * 360, Random.value * 360, Random.value * 360));
    }
}
