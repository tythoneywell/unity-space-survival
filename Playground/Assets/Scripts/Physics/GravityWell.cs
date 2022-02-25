using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GravityWell : MonoBehaviour
{
    protected List<GravityWellObject> wellObjects = new List<GravityWellObject>();

    private void Start()
    {
        foreach(GravityWellObject wellObj in gameObject.GetComponentsInChildren<GravityWellObject>())
        {
            EnterGravityWell(wellObj);
        }
    }

    void FixedUpdate()
    {
        foreach (GravityWellObject obj in wellObjects)
        {
            obj.ManualFixedUpdate();
        }
        ManualFixedUpdate();
    }

    public void EnterGravityWell(GravityWellObject wellObj)
    {
        wellObjects.Add(wellObj);
        wellObj.unbound = false;
        wellObj.transform.parent = gameObject.transform;
        wellObj.transform.rotation = gameObject.transform.rotation;
    }

    public void ExitGravityWell(GravityWellObject wellObj)
    {
        wellObj.unbound = true;
        wellObjects.Remove(wellObj);
    }

    protected abstract void ManualFixedUpdate();
}
