using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidField : MonoBehaviour
{

    [SerializeField]
    int maxAsteroids = 200;
    [SerializeField]
    Vector3 baseDrift = new Vector3(10, 2, -30);

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
    GameObject refAsteroid;
    
    List<GameObject> asteroids;
    Vector3 shipSpeed;


    // Start is called before the first frame update
    void Start()
    {
        asteroids = new List<GameObject>();
        shipSpeed = Vector3.zero;

        InitField();
    }

    // Update is called once per frame
    void Update()
    {
        CullField();
        GrowField();
    }

    // Instantiates a new asteroid with some randomized properties
    // And adds it to the list
    GameObject MakeAsteroid()
    {
        GameObject newAsteroid = GameObject.Instantiate(refAsteroid, transform);

        Rigidbody asteroidRB = newAsteroid.GetComponent<Rigidbody>();
        asteroidRB.AddTorque(Random.insideUnitSphere * randomSpinMax);
        asteroidRB.AddForce(baseDrift + shipSpeed + Random.insideUnitSphere * randomDriftMax, ForceMode.Acceleration);

        asteroids.Add(newAsteroid);
        return newAsteroid;
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
        for (int i = asteroids.Count - 1; i >= 0; i--)
        {
            if (Vector3.Distance(asteroids[i].transform.position, transform.position) > maxSpawnDist + 1)
            {
                GameObject.Destroy(asteroids[i]);
                asteroids.RemoveAt(i);
            }
                
        }

    }
    
    void GrowField()
    {
        for (int i = asteroids.Count; i < maxAsteroids; i++)
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
        foreach (GameObject a in asteroids)
        {
            a.GetComponent<Rigidbody>().AddForce(-panSpeed - shipSpeed, ForceMode.Acceleration);
            //a.transform.Translate(-panSpeed * Time.deltaTime, Space.World);
        }
        shipSpeed = -panSpeed;

    }
}
