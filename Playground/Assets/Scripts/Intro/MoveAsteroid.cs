using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAsteroid : MonoBehaviour
{
    public bool isTriggered = false;
    public float moveSpeed = 10;
    Vector3 initialPos;
    ParticleSystem explosionPs;


    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(isTriggered){
            Vector3 move = initialPos * -1f * Time.deltaTime;
            transform.Translate(move, Space.World);
            if (transform.position.x < 10){
                isTriggered = false;
                gameObject.SetActive(false);
                explosionPs = GameObject.Find("ExplosionSystem").GetComponent<ParticleSystem>();
                explosionPs.Play();
            }
        }
    }
}
