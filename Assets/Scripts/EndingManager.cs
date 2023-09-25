using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    [SerializeField]
    private DialogueData LanzallamasEndingData;
    [SerializeField]
    private DialogueData BarcelonaEndingData;
    [SerializeField]
    private DialogueData CuruxaEndingData;
    [SerializeField]
    private DialogueData PochilloEndingData;

    public UnityEngine.Events.UnityEvent event_;

    public void PlayLanzallamasEnding()
    {
        Dictionary<string, UnityEngine.Events.UnityEvent> dict = new Dictionary<string, UnityEngine.Events.UnityEvent>();
        dict.Add("start", event_);

        GameManager.Instance.GetComponent<FMODUnity.AudioScript>().StopNarradorSound();
        GameManager.Instance.GetComponent<FMODUnity.AudioScript>().PlayNarradorSound(29);

        DialogueManager.instance_.StartDialogue(LanzallamasEndingData, dict);
    }
    public void PlayBarcelonaEnding()
    {
        Dictionary<string, UnityEngine.Events.UnityEvent> dict = new Dictionary<string, UnityEngine.Events.UnityEvent>();
        dict.Add("start", event_);

        GameManager.Instance.GetComponent<FMODUnity.AudioScript>().StopNarradorSound();
        GameManager.Instance.GetComponent<FMODUnity.AudioScript>().PlayNarradorSound(25);

        DialogueManager.instance_.StartDialogue(BarcelonaEndingData, dict);
    }
    public void PlayCuruxaEnding()
    {
        Dictionary<string, UnityEngine.Events.UnityEvent> dict = new Dictionary<string, UnityEngine.Events.UnityEvent>();
        dict.Add("start", event_);

        GameManager.Instance.GetComponent<FMODUnity.AudioScript>().StopNarradorSound();
        GameManager.Instance.GetComponent<FMODUnity.AudioScript>().PlayNarradorSound(27);

        DialogueManager.instance_.StartDialogue(CuruxaEndingData, dict);
    }
    public void PlayPochilloEnding()
    {
        //Unity Event

        Dictionary<string, UnityEngine.Events.UnityEvent> dict = new Dictionary<string, UnityEngine.Events.UnityEvent>();
        dict["start"] =  event_;

        GameManager.Instance.GetComponent<FMODUnity.AudioScript>().StopNarradorSound();
        GameManager.Instance.GetComponent<FMODUnity.AudioScript>().PlayNarradorSound(26);

        /*
         * 
         * 
         */

        DialogueManager.instance_.StartDialogue(PochilloEndingData, dict);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("preload");
    }
}
