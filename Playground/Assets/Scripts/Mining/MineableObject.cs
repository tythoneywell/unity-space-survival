using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Root class for all GameObjects which can be mined via the spaceship laser.
 */
public class MineableObject : MonoBehaviour, IMineable
{
    public float maxHealth = 100;
    public ItemStack[] rewardMaterials;
    public GameObject[] debris;
    public float debrisScatterForce = 100f;
    public GameObject miniAsteroid;

    private float currHealth;

    // Start is called before the first frame update
    protected void Start()
    {
        currHealth = maxHealth;
    }

    public void DamageHealth(float laserStrength)
    {
        //Debug.LogFormat("decreased from {0} to {1}", currHealth, currHealth - laserStrength);
        currHealth = currHealth - laserStrength;

        if (currHealth < 0)
        {
            Break();
        }
    }

    void Break()
    {
        foreach (ItemStack stack in rewardMaterials)
            PlayerInventory.main.AddItemNoIndex(new ItemStack(stack));

        PlayerUIController.main.UpdateInventory();

        // Spawn debris chunks
        foreach (GameObject debrisPiece in debris)
        {
            Vector3 chunkBreakDirection = Random.insideUnitSphere;
            GameObject instantiatedDebris = Instantiate(debrisPiece, transform.position + transform.rotation * Vector3.Scale(chunkBreakDirection, transform.localScale), Quaternion.identity);
            instantiatedDebris.GetComponent<Rigidbody>().AddForce(transform.rotation * chunkBreakDirection * debrisScatterForce + gameObject.GetComponent<Rigidbody>().velocity, ForceMode.VelocityChange);
            AsteroidField.main.AddDebris(instantiatedDebris);
        }
        //TODO: randomize whether or not medium chunks exist to continue mining
        //Instantiate(miniAsteroid, new Vector3(transform.localPosition.x + 1, transform.localPosition.y, transform.localPosition.z), Quaternion.identity);
        //Instantiate(miniAsteroid, new Vector3(transform.localPosition.x - 1, transform.localPosition.y, transform.localPosition.z), Quaternion.identity);

        //Destroy the big asteroid
        //Debug.Log("asteroid destroyed");
        Destroy(gameObject);
    }
    public void BreakNoReward(Vector3 debrisForceDirection)
    {
        // Spawn debris chunks
        foreach (GameObject debrisPiece in debris)
        {
            Vector3 chunkBreakDirection = Random.insideUnitSphere;
            GameObject instantiatedDebris = Instantiate(debrisPiece, transform.position + transform.rotation * Vector3.Scale(chunkBreakDirection, transform.localScale), Quaternion.identity);
            instantiatedDebris.GetComponent<Rigidbody>().AddForce(transform.rotation * chunkBreakDirection * debrisScatterForce + debrisForceDirection + gameObject.GetComponent<Rigidbody>().velocity, ForceMode.VelocityChange);
            if (instantiatedDebris.name != "TinyDebris") AsteroidField.main.AddDebris(instantiatedDebris);
        }
        //TODO: randomize whether or not medium chunks exist to continue mining
        //Instantiate(miniAsteroid, new Vector3(transform.localPosition.x + 1, transform.localPosition.y, transform.localPosition.z), Quaternion.identity);
        //Instantiate(miniAsteroid, new Vector3(transform.localPosition.x - 1, transform.localPosition.y, transform.localPosition.z), Quaternion.identity);

        //Destroy the big asteroid
        //Debug.Log("asteroid destroyed");
        Destroy(gameObject);
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
