using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public enum DialogType{ Pass, Fail, Loop }

[CreateAssetMenu(fileName = "New Item", menuName = "FreeStylers/Spain Indie Game Jam 23/Create Item")]
public class ItemScriptableObject : ScriptableObject
{
    [SerializeReference]
    public string itemID;
    [SerializeReference]
    public List<Dialogue> dialogues;
}