using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomWrapper : MonoBehaviour
{
    public bool operational;
    public int health;

    public GameObject[] roomCenterpieces;

    public enum RoomType
    {
        EMPTY,
        FOOD,
        SMELTING,
        POWER,
        SHIELD,
    }
    public RoomType roomType = RoomType.EMPTY;

    RoomBackend roomBackend = new EmptyRoom();
    RoomCenterpiece roomCenterpiece;

    public void Build(RoomRecipe recipe)
    {
        roomBackend = recipe.room;

        if (roomCenterpiece != null) Destroy(roomCenterpiece.gameObject);
        roomCenterpiece = Instantiate(recipe.roomCenterpiece, transform).GetComponent<RoomCenterpiece>();
    }

    public void Interact(PlayerInteraction presser)
    {

    }
}
