using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IntegratedTurretController : MonoBehaviour
{
    public IntegratedTurret[] controlledTurrets;

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("clicked"); //debug
            foreach (IntegratedTurret turret in controlledTurrets)
            {
                turret.StartLaser();
            }
        }

        if (context.canceled)
        {
            foreach (IntegratedTurret turret in controlledTurrets)
            {
                turret.StopLaser();
            }
        }
    }


}
