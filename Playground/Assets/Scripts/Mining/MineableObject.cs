using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class is for testing. It is attached to the standard Asteroid prefab 
 * and allows us to simply shoot and destroy an asteroid.
 * 
 * It does NOT add to the inventory or generate smaller asteroids to mine.
 */
public class MineableObject : MonoBehaviour, IMineable
{
    public GameObject debris;
    public GameObject miniAsteroid;
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
                Instantiate(debris, new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z), Quaternion.identity);

            }
            //TODO: randomize whether or not medium chunks exist to continue mining
            //Instantiate(miniAsteroid, new Vector3(transform.localPosition.x + 1, transform.localPosition.y, transform.localPosition.z), Quaternion.identity);
            //Instantiate(miniAsteroid, new Vector3(transform.localPosition.x - 1, transform.localPosition.y, transform.localPosition.z), Quaternion.identity);
            
            //TODO: ADD MINERALS TO INVERNTORY


            //PlayerInteraction.main.inventory.AddItem(TODO)


            //Destroy the big asteroid
            Debug.Log("asteroid destroyed");
            Destroy(gameObject);
        }
    }
}





/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//old
public class MineableObject : MonoBehaviour, IMineable
{
    private GameObject debris;
    private GameObject miniAsteroid;
    private int maximumMineableAmount;
    private float health;

    private string minedItemType;
    private ItemData item;
    private ItemStack itemStack;

    public MineableObject(string minedItemType, float health, int maximumMineableAmount, GameObject debris, GameObject miniAsteroid)
    {
        this.minedItemType = minedItemType;
        this.health = health;
        this.maximumMineableAmount = maximumMineableAmount;
        this.debris = debris;
        this.miniAsteroid = miniAsteroid;
    }

    // Start is called before the first frame update
    void Start()
    {
        item = new ItemData();
        item.displayName = minedItemType;
        item.name = minedItemType;
        //TODO: variable max stack size????
        //Create new ItemStack with copper item and randomized integer amount
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
            //Instantiate(miniAsteroid, new Vector3(transform.localPosition.x + 1, transform.localPosition.y, transform.localPosition.z), Quaternion.identity);
            //Instantiate(miniAsteroid, new Vector3(transform.localPosition.x - 1, transform.localPosition.y, transform.localPosition.z), Quaternion.identity);

            //TODO: ADD MINERALS TO INVERNTORY
            PlayerInteraction.main.AddToInventory(itemStack);

            //Destroy the big asteroid
            Debug.Log("asteroid destroyed");
            Destroy(gameObject);
        }
    }
}*/
