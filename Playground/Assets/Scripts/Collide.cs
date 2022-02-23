using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collide : MonoBehaviour
{
    [SerializeField]
    Material material;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     void OnCollisionEnter (Collision other) 
     {
         Vector3 velo = other.relativeVelocity;
         if (velo.magnitude > 4){
            float red_val = material.color.r;
            Color color = material.color;
            color.r -= 0.1f;
            material.color = color;
         }
     }
}
