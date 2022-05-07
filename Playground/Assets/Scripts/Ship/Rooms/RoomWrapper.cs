using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Contains generic functionality of a room,
 * and a reference to its backend functionality
 */
public class RoomWrapper : ShipSystem
{
    public static RoomWrapper curr;

    const bool debug = false;

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
    public RoomRecipe startingRecipe = null;
    public RoomRecipe roomRecipe = null;

    public ProcessingRecipe defaultOxyRecipe;

    public bool built = false;

    RoomBackend roomBackend = new EmptyRoom();
    RoomCenterpiece roomCenterpiece;

    private void Start()
    {
        if (startingRecipe != null) {

            roomRecipe = startingRecipe;

            switch (startingRecipe.roomType)
            {
                case RoomType.SOLAR:
                    roomBackend = new SolarRoom();
                    break;
                case RoomType.SHIELD:
                    roomBackend = new ShieldRoom();
                    break;
                case RoomType.FARM:
                    roomBackend = new FarmRoom();
                    break;
                case RoomType.SMELTER:
                    roomBackend = new ForgeRoom();
                    break;
                case RoomType.ELECTROLYZER:
                    roomBackend = new OxygenRoom();
                    break;
                default:
                    roomBackend = new EmptyRoom();
                    break;
            }

            roomBackend.wrapper = this;
            roomBackend.Build();

            built = true;
            operational = true;
            health = 10;
            roomCenterpiece = Instantiate(startingRecipe.roomCenterpiece, transform).GetComponent<RoomCenterpiece>();
            if (startingRecipe.roomType == RoomType.ELECTROLYZER) ((OxygenRoom)roomBackend).InitAsStartRoom();
        }
    }

    private void Update()
    {
        if (operational)
        {
            roomBackend.Update();
            if (roomCenterpiece != null) roomCenterpiece.working = working;
        }
        else
        {
            powerProduction = 0f;
            powerConsumption = 0f;
            shieldCapacity = 0f;
            shieldChargeRate = 0f;
            working = false;
            if (roomCenterpiece != null) roomCenterpiece.working = working;
        }
    }

    public void Build(RoomRecipe recipe)
    {
        if (!PlayerInventory.main.HasRecipeIngredients(recipe) && !debug)
        {
            // Notify player that room can't be built
            Debug.Log("insufficient ingredients to build " + recipe.recipeName);
            return;
        }
        PlayerInventory.main.ConsumeRecipeIngredients(recipe);

        roomRecipe = recipe;

        roomBackend.Deconstruct();
        switch (recipe.roomType)
        {
            case RoomType.SOLAR:
                roomBackend = new SolarRoom();
                break;
            case RoomType.SHIELD:
                roomBackend = new ShieldRoom();
                break;
            case RoomType.FARM:
                roomBackend = new FarmRoom();
                break;
            case RoomType.SMELTER:
                roomBackend = new ForgeRoom();
                break;
            case RoomType.ELECTROLYZER:
                roomBackend = new OxygenRoom();
                break;
            default:
                roomBackend = new EmptyRoom();
                break;
        }
        roomBackend.wrapper = this;
        roomBackend.Build();

        built = true;
        operational = true;
        health = 10;

        if (roomCenterpiece != null) Destroy(roomCenterpiece.gameObject);
        roomCenterpiece = Instantiate(recipe.roomCenterpiece, transform).GetComponent<RoomCenterpiece>();
    }

    public void Interact(PlayerInteraction presser)
    {
        roomBackend.Interact(presser);
    }

    public bool Repair(int amount)
    {
        if (health >= 10) return false;
        else
        {
            health = Mathf.Clamp(health + amount, 0, 10);
            operational = true;
            return true;
        }
    }
}
