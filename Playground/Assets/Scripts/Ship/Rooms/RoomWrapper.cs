using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Contains generic functionality of a room,
 * and a reference to its backend functionality
 */
public class RoomWrapper : ShipSystem
{
    public enum RoomType
    {
        EMPTY,
        STORAGE,
        FARM,
        ELECTROLYZER,
        PLANTOXYGEN,
        SMELTER,
        SOLAR,
        BIOREACTOR,
        SHIELD,
    }
    public RoomType startingRoomType = RoomType.EMPTY;

    public bool built = false;

    RoomBackend roomBackend = new EmptyRoom();
    RoomCenterpiece roomCenterpiece;

    private void Update()
    {
        roomBackend.Update();
        if (roomCenterpiece != null) roomCenterpiece.working = working;
    }

    public void Build(RoomRecipe recipe)
    {
        if (PlayerInventory.main.HasRecipeIngredients(recipe))
        {
            PlayerInventory.main.ConsumeRecipeIngredients(recipe);
        }
        else
        {
            // Notify player that room can't be built
            Debug.Log("insufficient ingredients to build " + recipe.recipeName);
            return;
        }
        roomBackend.Deconstruct();
        switch (recipe.roomType)
        {
            case RoomType.SOLAR:
                roomBackend = new PowerRoom();
                break;
            case RoomType.SHIELD:
                roomBackend = new ShieldRoom();
                break;
            case RoomType.FARM:
                roomBackend = new FarmRoom();
                break;
            case RoomType.SMELTER:
                roomBackend = new FarmRoom();
                break;
            default:
                roomBackend = new EmptyRoom();
                break;
        }
        roomBackend.wrapper = this;
        roomBackend.Build();

        built = true;

        if (roomCenterpiece != null) Destroy(roomCenterpiece.gameObject);
        roomCenterpiece = Instantiate(recipe.roomCenterpiece, transform).GetComponent<RoomCenterpiece>();
    }

    public void Interact(PlayerInteraction presser)
    {
        roomBackend.Interact(presser);
    }
}
