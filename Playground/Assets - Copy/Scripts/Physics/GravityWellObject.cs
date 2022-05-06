using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GravityWellObject : MonoBehaviour
{
    public bool unbound = true;

    private void FixedUpdate()
    {
        // Can optionally change this later to behavior when floating in space
        if (unbound) ManualFixedUpdate();
    }

    public abstract void ManualFixedUpdate();
}
