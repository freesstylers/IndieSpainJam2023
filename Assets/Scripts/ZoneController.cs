using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneController : MonoBehaviour
{
    public static ZoneController Instance { get; private set; }

    public Zone[] ZoneList;

    private Dictionary<string, Zone> zoneDic;

    public string startingZone;

    [SerializeField]
    public string currentZone;

    [SerializeField]
    public float fadeInTime;
    [SerializeField]
    public float fadeOutTime;

    private string nextZone;

    private int currentZoneDay;
    private DayTime currentZoneDayTime;

    [Serializable]
    public struct EnabledObjects
    {
        public GameObject[] Morning;
        public GameObject[] Afternoon;
        public GameObject[] Night;
    }

    [Serializable]
    public struct Zone
    {
        public string zoneName;
        public GameObject zoneDay;
        public GameObject zoneTarde;
        public GameObject zoneNight;

        public EnabledObjects[] EnabledObjectsByDay;
    }
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

            //Esto no se si har� falta borrarlo
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        currentZone = startingZone;
        zoneDic = new Dictionary<string, Zone>();
        foreach(Zone z in ZoneList)
        {
            zoneDic.Add(z.zoneName, z);
        }
    }

    public void SetActiveZone(string zone, int day, DayTime dayTime, bool active)
    {
        if (!zoneDic.ContainsKey(zone)) return;
        Zone z = zoneDic[zone];
        switch (dayTime)
        {
            case DayTime.MORNING:
                z.zoneDay.SetActive(active);
                foreach (GameObject o in z.EnabledObjectsByDay[day].Morning)
                {
                    o.SetActive(active);
                }
                break;
            case DayTime.AFTERNOON:
                z.zoneDay.SetActive(active);
                foreach (GameObject o in z.EnabledObjectsByDay[day].Afternoon)
                {
                    o.SetActive(active);
                }
                break;
            case DayTime.NIGHT:
                z.zoneNight.SetActive(active);
                foreach (GameObject o in z.EnabledObjectsByDay[day].Night)
                {
                    o.SetActive(active);
                }
                break;
        }
        if (active)
        {
            currentZone = zone;
            currentZoneDay = day;
            currentZoneDayTime = dayTime;
        }
    }
    public void ChangeZone(string newZone)
    {
        nextZone = newZone;
        Camera.main.GetComponent<CameraEffects>().FadeToBlack(fadeOutTime, ChangeZoneCallback);
    }

    public void ChangeZoneCallback()
    {
        SetActiveZone(currentZone, currentZoneDay, currentZoneDayTime, false);
        
        SetActiveZone(nextZone, TimeManager.Instance.currentGameDay, TimeManager.Instance.currentDayTime, true);

        Camera.main.GetComponent<CameraEffects>().FadeFromBlack(fadeInTime, null);
    }

}
