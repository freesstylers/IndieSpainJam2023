using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public Image black_;
    private bool changingScene_ = false;
    public float timeToFade_;
    private float timer;

    public void Awake()
    {
        //Check if there is savedata
    }

    public void Play()
    {
        black_.gameObject.SetActive(true);
        changingScene_ = true;
    }

    public void Twitter()
    {
        Application.OpenURL("https://twitter.com/FreeStylers_Dev");
    }

    public void GameJam()
    {
        Application.OpenURL("https://itch.io/jam/gift-jam-2021");
    }

    public void ItchIO()
    {
        Application.OpenURL("https://freestylers-studio.itch.io/");
    }

    public void ShowCredits()
    {

    }

    public void HideCredits()
    {

    }

    public void FadeOut()
    {

    }

    public void newGame()
    {

    }

    public void LoadGame()
    {

    }
}
