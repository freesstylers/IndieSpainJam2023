using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
        }
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
        if (currentDayTime != DayTime.NIGHT)
            currentDayTime++;

        else if (currentGameDay < maxDays)
        {
            currentGameDay++;
            currentDayTime = DayTime.MORNING;

            dayNumber.text = currentGameDay.ToString();

            GetComponent<Animator>().SetTrigger("Play");
        }
        //recargar zona con el tiempo actualizado
        Debug.Log("Día: " + currentGameDay + " " + currentDayTime.ToString());

        ZoneController.Instance.ChangeZone(ZoneController.Instance.startingZone);

        Debug.Log("FALTA AUDIO DE DIA");


        clockAnim.SetTrigger(currentDayTime.ToString());
    }
}
