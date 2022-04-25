using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomRecipe", menuName = "ScriptableObjects/RoomRecipe", order = 1)]
public class RoomRecipe : ScriptableObject
{
    public RoomBackend room;
    public ItemStack[] recipe;

    public string roomName;
    public Sprite icon;

    public GameObject roomCenterpiece;
}
