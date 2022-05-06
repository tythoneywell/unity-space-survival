using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomRecipe", menuName = "ScriptableObjects/RoomRecipe", order = 1)]
public class RoomRecipe : Recipe
{
    public RoomWrapper.RoomType roomType;

    public GameObject roomCenterpiece;
}
