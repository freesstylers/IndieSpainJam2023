using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableNPC : InteractableObject
{
    [SerializeField]
    private CharacterScriptableObject characterInfo;    
    [SerializeField]
    private DialogueManager dialogueManager;
    public EventTrigger ev;

    public override void InteractCallback()
    {
        //DialogueManager.instance_.StartDialogue(characterInfo.dialogues[0]);
        ev.CanTriggerEvent(new List<string>());
    }
}
