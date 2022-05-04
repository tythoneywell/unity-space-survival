using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Produces power 
 */
public class ShieldRoom : RoomBackend
{
    const float shieldCapacityAmt = 2f;
    const float shieldRechargeAmt = 0.4f;
    const float powerDraw = 1.5f;

    public override void Build()
    {

    }
    public override void Update()
    {
        wrapper.shieldCapacity = shieldCapacityAmt;
        wrapper.shieldChargeRate = shieldRechargeAmt;
        wrapper.powerConsumption = powerDraw;
    }
    public override void Deconstruct()
    {

    }
}
