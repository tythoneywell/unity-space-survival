using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingStation : InteractableObject
{
    // Start is called before the first frame update
    public Recipe[] recipeList;
    public Recipe[] validCrafts;
    bool isActive = false;
    PlayerInteraction activePlayer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive){
            CheckValid(activePlayer);
        }
    }
    public override void Interact(PlayerInteraction presser){
        activePlayer = presser;
        Debug.Log("OPEN INVENTORY");
        Debug.Log("CLOSE INVENTORY");
        OnDeactivate();
    }
    void OnDeactivate(){
        isActive = false;
    }
    List<Recipe> CheckValid(PlayerInteraction presser){
        List<Recipe> valid = new List<Recipe>();
        foreach (Recipe recipe in recipeList){
            if (Craft(presser.gameObject.GetComponent<Inventory>().ToArray(), recipe)){
                valid.Add(recipe);
            }
        }
        return valid;
    }

    bool Craft(ItemStack[] inputItems, Recipe recipe){
        for (int reqNum = 0; reqNum < recipe.requirements.Length; reqNum++){
            ItemStack requirement = recipe.requirements[reqNum];
            for (int inputNum = 0; inputNum < inputItems.Length; inputNum++){
                ItemStack currStack = inputItems[inputNum];
                if (currStack.item == requirement.item){
                    if (currStack.count >= requirement.count && requirement.count > 0){
                        requirement.count = 0;
                        inputItems[inputNum].count -= requirement.count;
                    } else {
                        requirement.count -= currStack.count;
                        inputItems[inputNum].count = 0;
                    }
                }
            }
            if (requirement.count > 0) {
                return false;
            }
        }
        return true;
    }
}
