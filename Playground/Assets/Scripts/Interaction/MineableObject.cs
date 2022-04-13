using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineableObject : MonoBehaviour, IMineable
{
    public GameObject leftHalf;
    public GameObject rightHalf;
    private float health;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamageHealth(float laserStrength) 
    {
        Debug.LogFormat("decreased from {0} to {1}", health, health - laserStrength);
        health = health - laserStrength;
        
        if (health < 0)
        {
            //TODO: SPAWN TWO HALVES OF THE ASTEROID
            for (int i = 0; i < 10; i++)
            {
                Instantiate(leftHalf, new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z), Quaternion.identity);

            }
            Instantiate(rightHalf, new Vector3(transform.localPosition.x + 1, transform.localPosition.y, transform.localPosition.z), Quaternion.identity);
            Instantiate(rightHalf, new Vector3(transform.localPosition.x - 1, transform.localPosition.y, transform.localPosition.z), Quaternion.identity);
            //Instantiate(leftHalf, new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z), Quaternion.identity);
            /*Instantiate(leftHalf, new Vector3(transform.localPosition.x - 3, transform.localPosition.y - 3, transform.localPosition.z - 3), Quaternion.identity);
            Instantiate(rightHalf, new Vector3(transform.localPosition.x + 3, transform.localPosition.y + 3, transform.localPosition.z + 3), Quaternion.identity);
            Instantiate(leftHalf, new Vector3(transform.localPosition.x - 2, transform.localPosition.y - 2, transform.localPosition.z - 2), Quaternion.identity);
            Instantiate(leftHalf, new Vector3(transform.localPosition.x + 2, transform.localPosition.y + 2, transform.localPosition.z + 2), Quaternion.identity);*/
            //TODO: ADD MINERALS TO INVERNTORY

            //Destroy the big asteroid
            Debug.Log("asteroid destroyed");
            Destroy(gameObject);
        }
    }
}
