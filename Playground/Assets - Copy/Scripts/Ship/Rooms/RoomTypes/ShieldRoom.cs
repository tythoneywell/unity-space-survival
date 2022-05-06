using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Produces shields 
 */
public class ShieldRoom : RoomBackend
{
    const float shieldCapacityAmt = 2f;
    const float shieldRechargeAmt = 0.4f;
    const float powerDraw = 3f;

    public override void Build()
    {
        wrapper.working = true;
    }
    public override void Update()
    {
        wrapper.shieldCapacity = shieldCapacityAmt;
        wrapper.shieldChargeRate = shieldRechargeAmt * ShipSystemController.main.powerSatisfaction;
        wrapper.powerConsumption = powerDraw;
    }
    public override void Deconstruct()
    {

    }
}
