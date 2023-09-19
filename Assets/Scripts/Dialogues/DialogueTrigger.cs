using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public CharacterScriptableObject test;

    List<KeyValuePair<int, string>> dialogues;

    DialogueManager dialogueManager;
    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = GameManager.Instance.dialogueManager;
    }


    public void TriggerDialogue(int num)
    {

    }
}
