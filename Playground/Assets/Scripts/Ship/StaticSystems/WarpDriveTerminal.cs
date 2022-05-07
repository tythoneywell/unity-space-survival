using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpDriveTerminal : RepairableObject
{
    public Recipe warp1;
    public Recipe warp2;
    public Recipe warp3;

    public override void Repair()
    {
        base.Repair();
        ShipController.main.Warp();
    }
    public override string GetInteractPrompt()
    {
        return "[RMB] to repair warp drive";
    }
}
