using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
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

    public GameObject player;
    // Start is called before the first frame update

    public GameObject optionContainer;
    public GameObject optionPrefab;

    public GameObject charNameContainer;

    public Image leftSprite;
    public Image rightSprite;
    public Image centerSprite;

    public Color unfocusedTint = new Color(0.5f, 0.5f, 0.5f, 0.2f);

    FMODUnity.AudioScript audioScript;

    void Awake()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        
        if (container != null)
            container.SetActive(false);

        audioScript = GameManager.Instance.GetComponent<FMODUnity.AudioScript>();

        //aSourceCharacterEffect = GetComponents<AudioSource>()[0];
        //aSourceTypingEffect = GetComponents<AudioSource>()[1];

        if (instance_ == null)
            instance_ = this;
    }

    [SerializeField]
    Dictionary<string, DialogueTree> currentTree;
    [SerializeField]
    DialogueTree currentBranch;

    Dictionary<string, UnityEvent> events;

    int index = 0;

    public void StartDialogue(DialogueData data, Dictionary<string, UnityEvent> eventDict)
    {
        dialogueOver = false;
        currentTree = data.GetDialogueTree();
        events = new Dictionary<string, UnityEvent>(eventDict);

        StartBranch(data.dialogueTreeStart);
    }

    string currentBranchName;

    public void StartBranch(string branch)
    {
        CleanOptions();
        currentBranch = currentTree[branch];
        currentBranchName = branch;
        dialogueOver = false;
        index = 0;

        container.SetActive(true);
        player.GetComponent<Player.PlayerMovement>().SetInteracting(true);
        //Activate UI
        text.text = "";

        if(currentBranch.leftSprite != null)
        {
            leftSprite.sprite = currentBranch.leftSprite.sprite1;
            leftSprite.color = unfocusedTint;

            leftSprite.GetComponent<DialogueSpriteManager>().sprite1 = currentBranch.leftSprite.sprite1;
            leftSprite.GetComponent<DialogueSpriteManager>().sprite2 = currentBranch.leftSprite.sprite2;
        }


        if (currentBranch.centerSprite != null)
        {
            centerSprite.sprite = currentBranch.centerSprite.sprite1;
            centerSprite.color = unfocusedTint;

            centerSprite.GetComponent<DialogueSpriteManager>().sprite1 = currentBranch.centerSprite.sprite1;
            centerSprite.GetComponent<DialogueSpriteManager>().sprite2 = currentBranch.centerSprite.sprite2;
        }


        if (currentBranch.rightSprite != null)
        {
            rightSprite.sprite = currentBranch.rightSprite.sprite1;
            rightSprite.color = unfocusedTint;

            rightSprite.GetComponent<DialogueSpriteManager>().sprite1 = currentBranch.rightSprite.sprite1;
            rightSprite.GetComponent<DialogueSpriteManager>().sprite2 = currentBranch.rightSprite.sprite2;
        }

        leftSprite.gameObject.SetActive(currentBranch.leftSprite != null);
        centerSprite.gameObject.SetActive(currentBranch.centerSprite != null);
        rightSprite.gameObject.SetActive(currentBranch.rightSprite != null);

        charNameContainer.GetComponentInChildren<TextMeshProUGUI>().text = "";
        charNameContainer.SetActive(false);


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

            switch (currentBranch.dialogue[index].focusOnSide)
            {
                case Side.None:
                    leftSprite.color = unfocusedTint;
                    centerSprite.color = unfocusedTint;
                    rightSprite.color = unfocusedTint;
                    charNameContainer.SetActive(false);
                    break;
                case Side.Left:
                    leftSprite.color = Color.white;
                    leftSprite.transform.SetAsLastSibling();
                    centerSprite.color = unfocusedTint;
                    rightSprite.color = unfocusedTint;
                    charNameContainer.SetActive(true);

                    if(currentBranch.leftSprite != null)
                        charNameContainer.GetComponentInChildren<TextMeshProUGUI>().text = currentBranch.leftSprite.charName;

                    break;
                case Side.Center:
                    leftSprite.color = unfocusedTint;
                    centerSprite.color = Color.white;
                    centerSprite.transform.SetAsLastSibling();
                    rightSprite.color = unfocusedTint;
                    charNameContainer.SetActive(true);

                    if (currentBranch.centerSprite != null)
                        charNameContainer.GetComponentInChildren<TextMeshProUGUI>().text = currentBranch.centerSprite.charName;

                    break;
                case Side.Right:
                    leftSprite.color = unfocusedTint;
                    centerSprite.color = unfocusedTint;
                    rightSprite.color = Color.white;
                    rightSprite.transform.SetAsLastSibling();
                    charNameContainer.SetActive(true);

                    if (currentBranch.rightSprite != null)
                        charNameContainer.GetComponentInChildren<TextMeshProUGUI>().text = currentBranch.rightSprite.charName;

                    break;
                case Side.Keep:
                default:
                    break;
            }

            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
                displayLineCoroutine = null;
            }

            string s = DialogueKeyHandler.Instance.GetText(currentBranch.dialogue[index].key);

            string history = s;

            if (currentBranch.dialogue[index].focusOnSide != Side.None && charNameContainer.GetComponentInChildren<TextMeshProUGUI>().text != "")
                history = charNameContainer.GetComponentInChildren<TextMeshProUGUI>().text + ": " + s;

            GameManager.Instance.AddToHistory(history);
            //AudioClip audioToPlay = currentBranch.dialogue[index].audioToPlay;

            if(!currentBranch.dialogue[index].eventReference.IsNull)
                audioScript.PlaySound(currentBranch.dialogue[index].eventReference); //PlaySound


            index++;

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
                bool active = true;

                foreach (string i in o.itemNeeded)
                {
                    active &= GameManager.Instance.GetInventory().CheckItem(i);

                    if (!active)
                        break;
                }

                if (!active && o.hideOptionIfUnable)
                    continue;

                GameObject g = Instantiate(optionPrefab, optionContainer.transform);

                g.GetComponent<DialogueOptionButton>().Init(o, active);
            }
        }
    }

    public void EndDialogue()
    {
        CleanOptions();
        container.SetActive(false);
        player.GetComponent<Player.PlayerMovement>().SetInteracting(false);

        if(events.ContainsKey(currentBranchName))
            events[currentBranchName].Invoke();
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

        bool wordPlayed = false;

        foreach (char letter in letters)
        {
            if(!wordPlayed)
            {
                audioScript.PlaySound(audioScript.Escribir);
                wordPlayed = true;
            }

            if (letter == ' ')
            {
                wordPlayed = false;
            }

            text.text += letter;

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
