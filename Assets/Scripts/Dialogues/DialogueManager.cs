using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    TMPro.TextMeshProUGUI text;
    [SerializeField]
    Queue<string> sentences;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        StartDialogue(GetComponent<DialogueTrigger>().test.dialogues[0]);
    }

    public void StartDialogue(Dialogue d_)
    {
        foreach (string s in d_.fragments)
        {
            sentences.Enqueue(s);
        }

        text.gameObject.SetActive(true);
        //Activate UI
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count > 0)
        {
            string s = sentences.Dequeue();

            //UI to show s;
            text.text = s;
        }
        else
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        //Deactivate UI
        text.gameObject.SetActive(false);
    }
}
