using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Dialogue Data", menuName = "FreeStylers/Spain Indie Game Jam 23/Dialogue Data")]

public class DialogueData : ScriptableObject
{
    public enum Side { Keep, None, Left, Center, Right };

    [Serializable]
    public struct DialogueTreeListObject
    {
        public string branchName;
        [Header("")]
        public DialogueTree dialogueTree;
    }
    [Serializable]
    public struct DialogueTree
    {
        public List<DialogueText> dialogue;
        public Sprite leftSprite;
        public Sprite centerSprite;
        public Sprite rightSprite;
        [Header("")]
        public List<DialogueOption> options;
    }
    [Serializable]
    public struct DialogueText
    {
        public string key;
        public AudioClip audioToPlay;
        [Header("")]
        public Side focusOnSide;
    }
    [Serializable]
    public struct DialogueOption
    {
        public string key;
        [Header("")]
        public bool endDialogue;

        [Header("")]
        public string branchToJumpTo;
        public List<string> itemNeeded;
        public UnityEvent eventsToTrigger;
    }

    public string dialogueTreeStart;
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
