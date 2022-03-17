using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItemSpawner : MonoBehaviour
{
    [SerializeField]
    ItemData item;

    void Start()
    {
        Instantiate(item.MakeDroppedInstance(1), transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
