using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "FreeStylers/Spain Indie Game Jam 23/Create Character")]

public class CharacterScriptableObject : ScriptableObject
{
    [SerializeReference]
    public string charName;
    [SerializeReference]
    public Sprite sprite1;
    [SerializeReference]
    public Sprite sprite2;
}
