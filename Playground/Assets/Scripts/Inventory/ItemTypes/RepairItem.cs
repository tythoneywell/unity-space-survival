using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RepairItem", menuName = "ScriptableObjects/RepairItem", order = 1)]
public class RepairItem: ItemData
{
    public int repairAmount;

    public override void Use(PlayerInteraction presser)
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)), out hit))
        {
            RoomTerminal roomTerminal = hit.collider.GetComponent<RoomTerminal>();
            if (roomTerminal != null)
            {
                if (roomTerminal.Repair(repairAmount)) presser.RemoveCurrentFromInv(1);
            }
        }
    }
}
