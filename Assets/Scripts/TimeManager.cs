using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum DayTime { MORNING, AFTERNOON, NIGHT};
public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }
    public int currentGameDay;
    public int maxDays;
    public DayTime currentDayTime;

    public Animator clockAnim;
    public GameObject newDayCanvas;
    public TextMeshProUGUI dayNumber;
    public Button timeChanger;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(this.gameObject);
        }
        else
        {
            //Singleton
            Instance = this;

            DontDestroyOnLoad(this.gameObject);

            if (dayNumber != null)
                dayNumber.text = (currentGameDay + 1).ToString();

            GetComponentInChildren<Animator>().SetTrigger("Play");
        }
    }

    private void Start()
    {
        
    }
    public int getCurrentDay()
    {
        return currentGameDay;
    }

    public DayTime getCurrentDayTime()
    {
        return currentDayTime;
    }

    public void AdvanceTime()
    {

        if (currentDayTime != DayTime.NIGHT) {
            currentDayTime++;
        }
        else if (currentGameDay < maxDays)
        {
            currentGameDay++;
            currentDayTime = DayTime.MORNING;

            dayNumber.text = (currentGameDay + 1).ToString();

            GetComponentInChildren<Animator>().SetTrigger("Play");
        }
        else if (currentGameDay == maxDays && timeChanger.interactable)
        {
            timeChanger.interactable = false;
            timeChanger.GetComponentInChildren<UseCSV>().Refesh("MUY_TARDE");
            return;
        }

        if (currentDayTime == DayTime.MORNING || currentDayTime == DayTime.AFTERNOON)
        {
            GameManager.Instance.GetComponent<FMODUnity.AudioScript>().PlayMusic(FMODUnity.AudioScript.HORARIO.DIA);
        }
        else
        {
            GameManager.Instance.GetComponent<FMODUnity.AudioScript>().PlayMusic(FMODUnity.AudioScript.HORARIO.NOCHE);
        }

        //recargar zona con el tiempo actualizado
        Debug.Log("Día: " + currentGameDay+1 + " " + currentDayTime.ToString());

        ZoneController.Instance.ChangeZone(ZoneController.Instance.startingZone);

        Debug.Log("FALTA AUDIO DE DIA");
    }

    public void UpdateClock()
    {
        clockAnim.SetTrigger(currentDayTime.ToString());
    }

    public void HideClock()
    {
        timeChanger.gameObject.SetActive(false);
    }

    public void ShowClock()
    {
        timeChanger.gameObject.SetActive(true);
    }
}
