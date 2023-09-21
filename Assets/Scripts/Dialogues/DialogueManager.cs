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
    public float dialogSpeed = 20;
    [SerializeField]
    public AudioClip[] typingSounds;

    public List<string> sentences; //Para poder comprobarlo desde el inspector

    public static DialogueManager instance_ = null;

    private AudioSource aSourceCharacterEffect;
    private AudioSource aSourceTypingEffect;
    private Coroutine displayLineCoroutine;

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new List<string>();
        player = GameObject.FindGameObjectWithTag("Player");

        aSourceCharacterEffect = GetComponents<AudioSource>()[0];
        aSourceTypingEffect = GetComponents<AudioSource>()[1];

        if (instance_ == null)
            instance_ = this;
    }

    public void StartDialogue(Dialogue d_, AudioClip startDialogEffect)
    {
        foreach (string s in d_.fragments)
        {
            sentences.Add(s);
        }
        if(startDialogEffect != null)
        {
            aSourceCharacterEffect.PlayOneShot(startDialogEffect);
        }
        DisplayNextSentence();

        text.gameObject.SetActive(true);
        nextDialogueButton.gameObject.SetActive(true);
        player.GetComponent<Player.PlayerMovement>().SetInteracting(true);
        //Activate UI
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count > 0)
        {
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }

            string s = sentences[0];
            sentences.RemoveAt(0);

            displayLineCoroutine = StartCoroutine(progressiveSentenceDisplay(s));
            //UI to show s;
            //text.text = s;
        }
        else
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        //Deactivate UI
        StopCoroutine(displayLineCoroutine);
        text.gameObject.SetActive(false);
        nextDialogueButton.gameObject.SetActive(false);
        player.GetComponent<Player.PlayerMovement>().SetInteracting(false);
    }

    private IEnumerator progressiveSentenceDisplay(string sentence)
    {
        text.text = "";
        char[] letters = sentence.ToCharArray();
        aSourceTypingEffect.clip = typingSounds[UnityEngine.Random.Range(0, typingSounds.Length)];
        aSourceTypingEffect.Play();
        foreach (char letter in letters)
        {
            if (letter == ' ')
            {
                aSourceTypingEffect.clip = typingSounds[UnityEngine.Random.Range(0, typingSounds.Length)];
                aSourceTypingEffect.Play();
            }
            text.text += letter;
            //aSourceTypingEffect.clip = typingSounds[UnityEngine.Random.Range(0, typingSounds.Length)];
            //aSourceTypingEffect.Play();
            yield return new WaitForSeconds(1/dialogSpeed);
        }
    }
}
