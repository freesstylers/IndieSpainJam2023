using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static DialogueData;

public class DialogueOptionButton : MonoBehaviour
{
    public string branch;
    public UnityEvent events;

    public TextMeshProUGUI text;

    public DialogueOption option;

    public void Init(DialogueOption o, bool isActive)
    {
        branch = o.branchToJumpTo;
        text.text = DialogueKeyHandler.Instance.GetText(o.key);

        GetComponent<Button>().interactable = isActive;

        option = o;
    }

    public void Trigger()
    {
        option.eventsToTrigger.Invoke();

        if (option.endDialogue)
            DialogueManager.instance_.EndDialogue();
        else
            DialogueManager.instance_.StartBranch(branch);
    }
}
