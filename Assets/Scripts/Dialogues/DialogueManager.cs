using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    TMPro.TextMeshProUGUI text;    
    [SerializeField]
    Button nextDialogueButton;
    [SerializeField]
    Queue<string> sentences;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();

    }

    public void StartDialogue(Dialogue d_)
    {
        foreach (string s in d_.fragments)
        {
            sentences.Enqueue(s);
        }

        text.text = sentences.Dequeue();
        text.gameObject.SetActive(true);
        nextDialogueButton.gameObject.SetActive(true);
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
        nextDialogueButton.gameObject.SetActive(false);
    }
}
