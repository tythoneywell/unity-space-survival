using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidField : MonoBehaviour
{
    public static AsteroidField main;

    [SerializeField]
    int maxAsteroids = 200;
    [SerializeField]
    Vector3 baseDrift = new Vector3(10, 2, -30);

    [SerializeField]
    float maxDebris = 30;

    [SerializeField]
    float initMinSpawnDist = 100;
    [SerializeField]
    float minSpawnDist = 600;
    [SerializeField]
    float maxSpawnDist = 610;

    [SerializeField]
    float randomDriftMax = 8;
    [SerializeField]
    float randomSpinMax = 15;

    [SerializeField]
    GameObject[] spawnList;
    [SerializeField]
    int[] spawnListWeights;
    int spawnListTotalWeight;

    [SerializeField]
    float shrinkStartDist;
    [SerializeField]
    float shrinkDist;

    List<GameObject> asteroidsList;
    List<GameObject> debrisList;
    Vector3 shipSpeed;


    void Awake()
    {
        main = this;
        Asteroid.shrinkStartDist = shrinkStartDist;
        Asteroid.shrinkDist = shrinkDist;
    }

    void Start()
    {
        asteroidsList = new List<GameObject>();
        debrisList = new List<GameObject>();
        shipSpeed = Vector3.zero;

        spawnListTotalWeight = 0;
        foreach (int i in spawnListWeights)
        {
            spawnListTotalWeight += i;
        }

        InitField();
    }

    void Update()
    {
        CullField();
        GrowField();
    }

    public void AddDebris(GameObject debris)
    {
        if (debrisList.Count >= maxDebris) // Might try making while, but runs the risk of infinite while
        {
            Destroy(debrisList[0]);
            debrisList.RemoveAt(0);
        }
        debrisList.Add(debris);
    }

    // Instantiates a new asteroid with some randomized properties
    // And adds it to the list
    GameObject MakeAsteroid()
    {
        GameObject newAsteroid = GameObject.Instantiate(ChooseAsteroidPrefab(), transform);

        Rigidbody asteroidRB = newAsteroid.GetComponent<Rigidbody>();
        asteroidRB.AddTorque(Random.insideUnitSphere * randomSpinMax);
        asteroidRB.AddForce(baseDrift + shipSpeed + Random.insideUnitSphere * randomDriftMax, ForceMode.Acceleration);

        asteroidsList.Add(newAsteroid);
        return newAsteroid;
    }
    GameObject ChooseAsteroidPrefab()
    {
        float roll = Random.value;
        int cumulativeWeight = 0;
        for (int i = 0; i < spawnListWeights.Length; i++)
        {
            cumulativeWeight += spawnListWeights[i];
            if (roll < (float)cumulativeWeight / spawnListTotalWeight) return spawnList[i];
        }
        // We somehow got past the end of the list
        return spawnList[0];
    }

    void InitField()
    {
        for (int i = 0; i < maxAsteroids; i++)
        {
            // Get random position
            Vector3 randomDir = Random.onUnitSphere;
            float randomDist = Random.Range(initMinSpawnDist, maxSpawnDist);

            // Add asteroid
            GameObject newAsteroid = MakeAsteroid();
            newAsteroid.transform.Translate(randomDir * randomDist);

        }

    }

    void CullField()
    {
        for (int i = asteroidsList.Count - 1; i >= 0; i--)
        {
            if (asteroidsList[i] == null)
            {
                asteroidsList.RemoveAt(i);
            }
            else if (Vector3.Distance(asteroidsList[i].transform.position, transform.position) > maxSpawnDist + 1)
            {
                Destroy(asteroidsList[i]);
                asteroidsList.RemoveAt(i);
            }
        }
        for (int i = debrisList.Count - 1; i >= 0; i--)
        {
            if (debrisList[i] == null)
            {
                debrisList.RemoveAt(i);
            }
            else if (Vector3.Distance(debrisList[i].transform.position, transform.position) > maxSpawnDist + 1)
            {
                Destroy(debrisList[i]);
                debrisList.RemoveAt(i);
            }
        }
    }
    
    void GrowField()
    {
        for (int i = asteroidsList.Count; i < maxAsteroids; i++)
        {
            // Get random position from the direction of oncoming asteroids.
            // Note that circle projection gives a more uniform distribution than
            // making a hemisphere from onUnitSphere.
            Vector3 randomDir = Random.insideUnitCircle; // Start with random point on circle
            randomDir.z = Mathf.Sqrt(1 - randomDir.sqrMagnitude); // Project into a hemisphere on +z
            float randomDist = Random.Range(minSpawnDist, maxSpawnDist);

            // Rotate the position to the direction asteroids are coming from
            Vector3 asteroidDir = baseDrift + shipSpeed + Random.insideUnitSphere * randomDriftMax;
            Quaternion hemiRotation = Quaternion.FromToRotation(-Vector3.forward, asteroidDir);
            randomDir = hemiRotation * randomDir;

            // Add asteroid
            GameObject newAsteroid = MakeAsteroid();
            newAsteroid.transform.Translate(randomDir * randomDist);
        }

    }

    public void PanField(Vector3 panSpeed)
    {
        foreach (GameObject a in asteroidsList)
        {
            if (a != null)
            {
                a.GetComponent<Rigidbody>().AddForce(-panSpeed - shipSpeed, ForceMode.Acceleration);
                //a.transform.Translate(-panSpeed * Time.deltaTime, Space.World);
            }
        }
        foreach (GameObject a in debrisList)
        {
            if (a != null)
            {
                a.GetComponent<Rigidbody>().AddForce(-panSpeed - shipSpeed, ForceMode.Acceleration);
                //a.transform.Translate(-panSpeed * Time.deltaTime, Space.World);
            }
        }
        shipSpeed = -panSpeed;

    }
}
