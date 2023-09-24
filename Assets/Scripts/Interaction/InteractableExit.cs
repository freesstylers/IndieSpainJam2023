using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableExit : InteractableObject
{
    public string toZone;

    public void Awake()
    {
        MouseHighlightOutline = 10;
        KeyHighlightOutline = 5;
    }
    public override void InteractCallback()
    {
        ZoneController.Instance.ChangeZone(toZone);
    }
}
