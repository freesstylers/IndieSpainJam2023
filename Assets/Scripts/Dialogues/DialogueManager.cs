using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.UI;
using static DialogueData;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    TMPro.TextMeshProUGUI text;

    public GameObject container;

    [SerializeField]
    public float dialogSpeed = 20;
    [SerializeField]
    public AudioClip[] typingSounds;

    public bool dialogueOver = true;

    public static DialogueManager instance_ = null;

    private AudioSource aSourceCharacterEffect;
    private AudioSource aSourceTypingEffect;
    private Coroutine displayLineCoroutine;

    private GameObject player;
    // Start is called before the first frame update

    public GameObject optionContainer;
    public GameObject optionPrefab;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        container.SetActive(false);

        //aSourceCharacterEffect = GetComponents<AudioSource>()[0];
        //aSourceTypingEffect = GetComponents<AudioSource>()[1];

        if (instance_ == null)
            instance_ = this;
    }

    [SerializeField]
    Dictionary<string, DialogueTree> currentTree;
    [SerializeField]
    DialogueTree currentBranch;

    int index = 0;

    public void StartDialogue(DialogueData data)
    {
        dialogueOver = false;
        currentTree = data.GetDialogueTree();

        StartBranch(data.dialogueTreeStart);
    }

    public void StartBranch(string branch)
    {
        CleanOptions();
        currentBranch = currentTree[branch];
        dialogueOver = false;
        index = 0;

        container.SetActive(true);
        player.GetComponent<Player.PlayerMovement>().SetInteracting(true);
        //Activate UI
        text.text = "";

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (dialogueOver)
            return;

        bool skip = false;
        //Deactivate UI
        if (displayLineCoroutine != null)
        {
            StopCoroutine(displayLineCoroutine);
            displayLineCoroutine = null;

            text.text = DialogueKeyHandler.Instance.GetText(currentBranch.dialogue[index - 1].key);
            skip = true;
        }

        if (skip)
            return;

        if (currentBranch.dialogue.Count > index)
        {
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
                displayLineCoroutine = null;
            }

            string s = DialogueKeyHandler.Instance.GetText(currentBranch.dialogue[index].key);
            AudioClip audioToPlay = currentBranch.dialogue[index].audioToPlay;
            index++;

            //PLAY AUDIO SI NO ES NULL!!!!!!!!!!!
            Debug.Log("FALTA EL PLAY DEL AUDIO DE DIÁLOGO!");

            displayLineCoroutine = StartCoroutine(progressiveSentenceDisplay(s));
        }
        else
        {
            TryEndDialogue();
        }
    }

    public void TryEndDialogue()
    {
        dialogueOver = true;

        if (currentBranch.options == null || currentBranch.options.Count == 0)
        {
            EndDialogue();
        }
        else
        {
            foreach (DialogueOption o in currentBranch.options)
            {
                GameObject g = Instantiate(optionPrefab, optionContainer.transform);

                bool active = true;

                foreach (string i in o.itemNeeded)
                {
                    active &= player.GetComponent<Inventory>().CheckItem(i);

                    if (!active)
                        break;
                }

                g.GetComponent<DialogueOptionButton>().Init(o, active);
            }
        }
    }

    public void EndDialogue()
    {
        CleanOptions();
        container.SetActive(false);
        player.GetComponent<Player.PlayerMovement>().SetInteracting(false);
    }

    public void CleanOptions()
    {
        foreach(Transform c in optionContainer.transform)
            Destroy(c.gameObject);
    }

    private IEnumerator progressiveSentenceDisplay(string sentence)
    {
        text.text = "";
        char[] letters = sentence.ToCharArray();
        //aSourceTypingEffect.clip = typingSounds[UnityEngine.Random.Range(0, typingSounds.Length)];
        //aSourceTypingEffect.Play();

        //PLAY AUDIO SI NO ES NULL!!!!!!!!!!!
        Debug.Log("FALTA EL PLAY DEL AUDIO DE TYPING!");

        foreach (char letter in letters)
        {
            if (letter == ' ')
            {
                //aSourceTypingEffect.clip = typingSounds[UnityEngine.Random.Range(0, typingSounds.Length)];
                //aSourceTypingEffect.Play();
            }
            text.text += letter;
            //aSourceTypingEffect.clip = typingSounds[UnityEngine.Random.Range(0, typingSounds.Length)];
            //aSourceTypingEffect.Play();
            yield return new WaitForSeconds(1/dialogSpeed);
        }

        displayLineCoroutine = null;
    }

    private void Update()
    {
    }

    public void ChooseOption(string option)
    {

    }
}
