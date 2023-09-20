using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableNPC : InteractableObject
{
    [SerializeField]
    private CharacterScriptableObject characterInfo;    
    [SerializeField]
    private DialogueManager dialogueManager;

    public override void InteractCallback()
    {
        dialogueManager.StartDialogue(characterInfo.dialogues[0]);
    }
}
