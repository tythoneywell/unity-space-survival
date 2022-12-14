using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidTrigger : MonoBehaviour
{
    float init;
    bool isTriggered = false;
    public MoveAsteroid bigAsteroid;
    ParticleSystem explosionPs;
    private void Awake(){
        init = Time.realtimeSinceStartup;
        explosionPs = GameObject.Find("ExplosionSystem").GetComponent<ParticleSystem>();
        explosionPs.emissionRate = 100;
    }
    private void Update(){
        float currtime = Time.realtimeSinceStartup;
        if (!isTriggered && (currtime - init) > 9.5){
            Debug.Log("---------8-------");
            bigAsteroid.isTriggered = true;
            isTriggered = true;
        }
    }
    // Start is called before the first frame update
    private void onTriggerEnter(Collider collider){
        Debug.Log("Boom");
    }
}
