using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This is the class to be worked on still before the rest of the types of
 * asteroids will be made. This will be the model.
 * 
 * 04/18/2022, adding the variable itemStack to inventory
 * results in a null pointer exception. This occurs despite having an ItemData
 * object for Copper, and an itemStack that shows that it is not NULL during
 * runtime. RESOLVED by adding all elements from MAIN SCENE
 * 
 * 04/21/2022: Exploding the asteroids successfully adds to inventory but UI does not update to show.
 * 
 * 04/26/2022: InputSystem now works. Inventory item count updates successfully after first asteroid is destroyed.
 * For some reason after the second asteroid is destroyed, there is no log output of a new item count.
 */
public class CopperAsteroid : MonoBehaviour, IMineable
{


    public GameObject debris;
    public GameObject miniAsteroid;
    public ItemData item;
    public int amount; //DEBUG TAKE OUT LATER

    private float health;
    //private ItemData item;
    public ItemStack itemStack;

    // Start is called before the first frame update
    void Start()
    {
        health = AsteroidProperties.CopperAsteroidHealth;
        //itemStack = new ItemStack(item, Random.Range(1, AsteroidProperties.MaxCopperMineableAmount));
        itemStack = new ItemStack(item, amount); //DEBUG DELETE AND UNCOMMENT ABOVE
    }

    public void DamageHealth(float laserStrength)
    {
        Debug.LogFormat("decreased from {0} to {1}", health, health - laserStrength);
        health = health - laserStrength;

        if (health < 0)
        {
            // Spawn multiple debris
            for (int i = 0; i < 10; i++)
            {
                Instantiate(debris, new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z), Quaternion.identity);

            }
            //TODO: randomize whether or not medium chunks exist to continue mining
            //Instantiate(miniAsteroid, new Vector3(transform.localPosition.x + 1, transform.localPosition.y, transform.localPosition.z), Quaternion.identity);
            //Instantiate(miniAsteroid, new Vector3(transform.localPosition.x - 1, transform.localPosition.y, transform.localPosition.z), Quaternion.identity);

            //TODO: ADD MINERALS TO INVERNTORY
            PlayerInteraction.main.AddToInventory(new ItemStack(itemStack));

            for (int i = 0; i <= 35; i++) {
                Debug.Log(PlayerInteraction.main.inventory.GetItem(i).item.itemName);
                Debug.Log(PlayerInteraction.main.inventory.GetItem(i).count);
            }
                      
            

            //PlayerInteraction.main.inventory.AddItem(TODO)


            //Destroy the big asteroid
            Debug.Log("asteroid destroyed");
            Destroy(gameObject);
        }
    }



    /*public GameObject debris;
    public GameObject miniAsteroid;
    public float health;

    private ItemData item;
    private ItemStack itemStack;

    // Start is called before the first frame update
    void Start()
    {
        item = new ItemData();
        item.displayName = "Copper";
        item.name = "Copper";
        //TODO: variable max stack size????
        //Create new ItemStack with copper item and randomized integer amount
        itemStack = new ItemStack(item, Random.Range(1, AsteroidProperties.MaxCopperMineableAmount));
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
            //Instantiate(miniAsteroid, new Vector3(transform.localPosition.x + 1, transform.localPosition.y, transform.localPosition.z), Quaternion.identity);
            //Instantiate(miniAsteroid, new Vector3(transform.localPosition.x - 1, transform.localPosition.y, transform.localPosition.z), Quaternion.identity);

            //TODO: ADD MINERALS TO INVERNTORY
            //PlayerInteraction.main.AddToInventory(itemStack);

            //Destroy the big asteroid
            Debug.Log("asteroid destroyed");
            Destroy(gameObject);
        }
    }*/
}
