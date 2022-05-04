using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Produces power 
 */
public class PowerRoom : RoomBackend
{
    const float powerAmt = 1f;

    public override void Build()
    {

    }
    public override void Update()
    {
        wrapper.powerProduction = powerAmt;
    }
    public override void Deconstruct()
    {

    }
}
