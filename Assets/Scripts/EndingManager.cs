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

    public void PlayLanzallamasEnding()
    {
        GameManager.Instance.GetComponent<FMODUnity.AudioScript>().StopNarradorSound();
        GameManager.Instance.GetComponent<FMODUnity.AudioScript>().PlayNarradorSound(29);

        DialogueManager.instance_.StartDialogue(LanzallamasEndingData);
    }
    public void PlayBarcelonaEnding()
    {
        GameManager.Instance.GetComponent<FMODUnity.AudioScript>().StopNarradorSound();
        GameManager.Instance.GetComponent<FMODUnity.AudioScript>().PlayNarradorSound(25);

        DialogueManager.instance_.StartDialogue(BarcelonaEndingData);
    }
    public void PlayCuruxaEnding()
    {
        GameManager.Instance.GetComponent<FMODUnity.AudioScript>().StopNarradorSound();
        GameManager.Instance.GetComponent<FMODUnity.AudioScript>().PlayNarradorSound(27);

        DialogueManager.instance_.StartDialogue(CuruxaEndingData);
    }
    public void PlayPochilloEnding()
    {
        GameManager.Instance.GetComponent<FMODUnity.AudioScript>().StopNarradorSound();
        GameManager.Instance.GetComponent<FMODUnity.AudioScript>().PlayNarradorSound(26);

        DialogueManager.instance_.StartDialogue(PochilloEndingData);
    }

    public static void ToMainMenu()
    {
        Application.Quit();
    }
}
