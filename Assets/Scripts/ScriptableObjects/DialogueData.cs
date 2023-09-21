using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Dialogue Data", menuName = "FreeStylers/Spain Indie Game Jam 23/Dialogue Data")]

public class DialogueData : ScriptableObject
{
    [Serializable]
    public struct DialogueTreeListObject
    {
        public string branchName;
        public DialogueTree dialogueTree;
    }
    [Serializable]
    public struct DialogueTree
    {
        public List<DialogueText> dialogue;
        public List<DialogueOption> options;
    }
    [Serializable]
    public struct DialogueText
    {
        public string key;
    }
    [Serializable]
    public struct DialogueOption
    {
        public string key;
        public List<string> itemNeeded;
        public List<string> flagNeeded;
        public string branchToJumpTo;
        public UnityEvent eventsToTrigger;
    }

    [SerializeField]
    private List<DialogueTreeListObject> dialogueTreeList;

    private Dictionary<string, DialogueTree> dialogueTree = null;

    public Dictionary<string, DialogueTree> GetDialogueTree()
    {
        if (dialogueTree == null)
        {
            dialogueTree = new Dictionary<string, DialogueTree>();

            foreach (var d in dialogueTreeList)
            {
                dialogueTree[d.branchName] = d.dialogueTree;
            }
        }
        
        return dialogueTree;
    }
}
