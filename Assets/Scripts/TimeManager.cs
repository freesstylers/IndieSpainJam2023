using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DayTime { MORNING, AFTERNOON, NIGHT};
public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }
    public int currentGameDay;
    public int maxDays;
    public DayTime currentDayTime;

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
        if(currentDayTime != DayTime.NIGHT)
            currentDayTime++;

        else if (currentGameDay < maxDays){
            currentGameDay++;
            currentDayTime = DayTime.MORNING;
        }
        //recargar zona con el tiempo actualizado
        ZoneController.Instance.LoadStartingZone();
    }
}
