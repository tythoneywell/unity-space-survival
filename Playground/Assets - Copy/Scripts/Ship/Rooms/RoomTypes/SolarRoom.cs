using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Produces power 
 */
public class SolarRoom : RoomBackend
{
    const float powerAmt = 5f;

    public override void Build()
    {
        wrapper.working = true;
    }
    public override void Update()
    {
        wrapper.powerProduction = powerAmt;
    }
    public override void Deconstruct()
    {

    }
}
