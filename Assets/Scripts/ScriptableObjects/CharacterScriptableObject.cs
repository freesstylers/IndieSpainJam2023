using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "FreeStylers/Spain Indie Game Jam 23/Create Character")]

public class CharacterScriptableObject : ScriptableObject
{
    [SerializeReference]
    public string characterIdentifier;
    [SerializeReference]
    public DialogueData dialogue;
}
