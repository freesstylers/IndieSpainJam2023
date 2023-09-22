using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableExit : InteractableObject
{
    public string toZone;
    public override void InteractCallback()
    {
        ZoneController.Instance.ChangeZone(toZone);
    }
}
