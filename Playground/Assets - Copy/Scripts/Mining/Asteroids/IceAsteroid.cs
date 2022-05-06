using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class has not yet been finished. Will finish when CopperAsteroid
 * properly adds Copper to the inventory.
 * 
 */
public class IceAsteroid : MonoBehaviour, IMineable
{
    public GameObject debris;
    public GameObject miniAsteroid;
    public int maximumMineableAmount;
    public float health;

    private ItemData item;
    private ItemStack itemStack;

    void Start()
    {
        item = new ItemData();
        item.displayName = "Water";
        item.name = "water";
        //TODO: variable max stack size????
        //Create new ItemStack with water item and randomized integer amount
        itemStack = new ItemStack(item, Random.Range(1, maximumMineableAmount));
    }

    public void DamageHealth(float laserStrength)
    {
        Debug.LogFormat("asteroid health decreased from {0} to {1}", health, health - laserStrength);
        health = health - laserStrength;

        if (health < 0)
        {
            //Spawn small rock pieces
            for (int i = 0; i < 10; i++)
            {
                Instantiate(debris, new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z), Quaternion.identity);

            }
            //TODO: randomize whether or not medium chunks exist to continue mining
            Instantiate(miniAsteroid, new Vector3(transform.localPosition.x + 1, transform.localPosition.y, transform.localPosition.z), Quaternion.identity);
            Instantiate(miniAsteroid, new Vector3(transform.localPosition.x - 1, transform.localPosition.y, transform.localPosition.z), Quaternion.identity);

            //TODO: ADD MINERALS TO INVERNTORY
            PlayerInteraction.main.AddToInventory(itemStack);



            //Destroy the big asteroid
            Debug.Log("asteroid destroyed");
            Destroy(gameObject);
        }
    }


}
