using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyExtraAsteroids : MonoBehaviour
{
    [SerializeField]
    public float distanceToDestroy;

    private Vector3 initial_position;

    // Start is called before the first frame update
    void Start()
    {
        initial_position = transform.position;
    }

    void DestroyIfDisplaced()
    {
        if (distanceTo(initial_position) >= distanceToDestroy)
        {
            Destroy(gameObject);
        }
    }

    float distanceTo(Vector3 initial)
    {
        float deltaX = transform.position.x - initial_position.x;
        float deltaY = transform.position.y - initial_position.y;
        float deltaZ = transform.position.z - initial_position.z;

        return Mathf.Sqrt(Mathf.Pow(deltaX, 2) + Mathf.Pow(deltaY, 2) + Mathf.Pow(deltaZ, 2));
    }

    // Update is called once per frame
    void Update()
    {
        DestroyIfDisplaced();
    }
}
