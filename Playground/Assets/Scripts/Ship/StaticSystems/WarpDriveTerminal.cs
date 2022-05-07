using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarpDriveTerminal : RepairableObject
{
    public int zone = 0;

    public Recipe warp1;
    public Recipe warp2;
    public Recipe warp3;

    public override void Repair()
    {
        zone++;
        base.Repair();
        if (zone == 0)
        {
            repairCost = warp1;
        }
        if (zone == 1)
        {
            repairCost = warp2;
        }
        if (zone == 2)
        {
            repairCost = warp3;
        }
        if (zone == 3)
        {
            SceneManager.LoadScene("OutroCutScene");
        }
        ShipController.main.Warp();
    }
    public override string GetInteractPrompt()
    {
        return "[RMB] to repair warp drive";
    }
}
