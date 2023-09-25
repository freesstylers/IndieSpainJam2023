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
        public EnabledObjects[] DisabledObjectsByDay;
        
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

            //Esto no se si hará falta borrarlo
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

        ChangeZone(currentZone);
    }

    public void SetActiveZone(string zone, int day, DayTime dayTime, bool active)
    {
        if (!zoneDic.ContainsKey(zone)) return;
        Zone z = zoneDic[zone];
        switch (dayTime)
        {
            case DayTime.MORNING:
                if (active)
                {
                    foreach (GameObject o in z.EnabledObjectsByDay[day].Morning)
                    {
                        o.SetActive(true);
                    }
                    foreach (GameObject o in z.DisabledObjectsByDay[day].Morning)
                    {
                        o.SetActive(false);
                    }
                }
                z.zoneDay.SetActive(active);
                break;
            case DayTime.AFTERNOON:
                if (active)
                {
                    foreach (GameObject o in z.EnabledObjectsByDay[day].Afternoon)
                    {
                        o.SetActive(true);
                    }
                    foreach (GameObject o in z.DisabledObjectsByDay[day].Afternoon)
                    {
                        o.SetActive(false);
                    }
                }
                z.zoneTarde.SetActive(active);
                break;
            case DayTime.NIGHT:
                if (active)
                {
                    foreach (GameObject o in z.EnabledObjectsByDay[day].Night)
                    {
                        o.SetActive(true);
                    }
                    foreach (GameObject o in z.DisabledObjectsByDay[day].Night)
                    {
                        o.SetActive(false);
                    }
                }
                z.zoneNight.SetActive(active);
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
        if (zoneDic.ContainsKey(newZone))
        {
            GameManager.Instance.GetComponent<FMODUnity.AudioScript>().CambiarEscenario(newZone, newZone);

            PlayNarradorSoundChangeZone(newZone);

            nextZone = newZone;
            if (Camera.main != null && Camera.main.GetComponent<CameraEffects>() != null)
            {
                Camera.main.GetComponent<CameraEffects>().FadeToBlack(fadeOutTime, ChangeZoneCallback);
            }
        }
        else Debug.Log("Zona no válida, asignar ToZone");
    }

    //Mirad lo que me me habeis obligado a hacer
    private void PlayNarradorSoundChangeZone(string newZone)
    {
        FMODUnity.AudioScript audio = GameManager.Instance.GetComponent<FMODUnity.AudioScript>();
        Debug.Log("Sound narrador " + newZone + " " + currentZoneDay + " " + currentZoneDayTime);
        switch (currentZoneDay){
            case 0:
                if (currentZoneDayTime == DayTime.NIGHT)
                {
                    switch (newZone)
                    {
                        case "bosque":
                            audio.PlayNarradorSound(0);
                            break;
                        case "iglesia":
                            audio.PlayNarradorSound(1);
                            break;
                        case "plaza":
                            audio.PlayNarradorSound(2);
                            break;
                        case "taberna":
                            audio.PlayNarradorSound(3);
                            break;
                    }
                }
                break;
                    
            case 1:
                if (currentZoneDayTime == DayTime.NIGHT)
                {
                    switch (newZone)
                    {
                        case "bosque":
                            audio.PlayNarradorSound(4);
                            break;
                        case "casa":
                            audio.PlayNarradorSound(5);
                            break;
                        case "iglesia":
                            audio.PlayNarradorSound(6);
                            break;
                        case "plaza":
                            audio.PlayNarradorSound(7);
                            break;
                        case "taberna":
                            audio.PlayNarradorSound(8);
                            break;
                    }
                }
                break;
            case 2:
                switch(currentZoneDayTime)
                {
                    case DayTime.MORNING:
                        switch (newZone)
                        {
                            case "casa":
                                audio.PlayNarradorSound(21);
                                break;
                            case "emporio":
                                audio.PlayNarradorSound(22);
                                break;
                            case "plaza":
                                audio.PlayNarradorSound(23);
                                break;
                        }
                        break;
                    case DayTime.AFTERNOON:
                        switch (newZone)
                        {
                            case "casa":
                                audio.PlayNarradorSound(16);
                                break;
                            case "iglesia":
                                audio.PlayNarradorSound(18);
                                break;
                            case "plaza":
                                audio.PlayNarradorSound(20);
                                break;
                        }
                        break;
                    case DayTime.NIGHT:
                        switch (newZone)
                        {
                            case "bosque":
                                audio.PlayNarradorSound(9);
                                break;
                            case "taberna":
                                audio.PlayNarradorSound(14);
                                break;
                        }
                        break;
                }

                break;
        }
    }

    public void ChangeZoneCallback()
    {
        SetActiveZone(currentZone, currentZoneDay, currentZoneDayTime, false);
        
        SetActiveZone(nextZone, TimeManager.Instance.currentGameDay, TimeManager.Instance.currentDayTime, true);

        CameraEffects cm = Camera.main.GetComponent<CameraEffects>();
        cm.FadeFromBlack(fadeInTime, null);
    }
}
