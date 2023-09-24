using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Video.VideoPlayer;

public class InteractableNPC : InteractableObject
{
    [SerializeField]
    private CharacterScriptableObject characterInfo;
    public GameEventHandler ev;
    public override void InteractCallback()
    {
        ev.TriggerEvent();
    }
}
