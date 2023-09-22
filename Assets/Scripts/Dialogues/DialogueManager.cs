using System.Collections;
using System.Collections.Generic;
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
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

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
        currentBranch = currentTree[data.dialogueTreeStart];
        index = 0;

        container.SetActive(true);
        player.GetComponent<Player.PlayerMovement>().SetInteracting(true);
        //Activate UI
        text.text = "";

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (currentBranch.dialogue.Count > index)
        {
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }

            string s = currentBranch.dialogue[index].key;
            AudioClip audioToPlay = currentBranch.dialogue[index].audioToPlay;
            index++;

            displayLineCoroutine = StartCoroutine(progressiveSentenceDisplay(s, audioToPlay));
        }
        else
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        if (currentBranch.options == null || currentBranch.options.Count <= index)
        {
            dialogueOver = true;
            //Deactivate UI
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }
            container.SetActive(false);
            player.GetComponent<Player.PlayerMovement>().SetInteracting(false);
        }
        else
        {
            //DISPLAY OPTIONS!!!!!!!!!!!
            Debug.Log("FALTA EL DISPLAY DE OPCIONES!");
        }
    }

    private IEnumerator progressiveSentenceDisplay(string sentence, AudioClip audioToPlay)
    {
        text.text = "";
        char[] letters = sentence.ToCharArray();
        //aSourceTypingEffect.clip = typingSounds[UnityEngine.Random.Range(0, typingSounds.Length)];
        //aSourceTypingEffect.Play();

        //PLAY AUDIO SI NO ES NULL!!!!!!!!!!!
        Debug.Log("FALTA EL PLAY DEL AUDIO DE DIÁLOGO!");
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
    }

    private void Update()
    {
    }
}
