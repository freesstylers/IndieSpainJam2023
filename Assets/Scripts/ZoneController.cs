using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneController : MonoBehaviour
{
    public static ZoneController Instance { get; private set; }

    public Zone[] ZoneList;

    public int startingZoneIndx = 0;

    [SerializeField]
    Zone currentZone;

    [Serializable]
    public struct Zone
    {
        public GameObject zoneMorning;
        public GameObject zoneAfternoon;
        public GameObject zoneNight;

        public GameObject[] EnabledItemsMorning;
        public GameObject[] EnabledItemsAfternoon;
        public GameObject[] EnabledItemsNight;

        public GameObject[] EnabledNPCsMorning;
        public GameObject[] EnabledNPCsAfternoon;
        public GameObject[] EnabledNPCsNight;
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
    public void ChangeZone(Zone z)
    {
        //Current zone
        if (currentZone.zoneMorning != null)
            currentZone.zoneMorning.SetActive(false);
        if (currentZone.zoneAfternoon != null)
            currentZone.zoneAfternoon.SetActive(false);
        if (currentZone.zoneNight != null)
            currentZone.zoneNight.SetActive(false);

        DayTime currentDayTime = TimeManager.Instance.getCurrentDayTime();

        switch (currentDayTime)
        {
            case DayTime.MORNING:
                foreach (GameObject item in currentZone.EnabledItemsMorning)
                {
                    if (item != null)
                        item.SetActive(false);
                }
                foreach (GameObject item in z.EnabledItemsMorning)
                {
                    if (item != null)
                        item.SetActive(true);
                }
                foreach (GameObject npc in currentZone.EnabledNPCsMorning)
                {
                    if (npc != null)
                        npc.SetActive(false);
                }
                foreach (GameObject npc in z.EnabledNPCsMorning)
                {
                    if (npc != null)
                        npc.SetActive(true);
                }
                if (z.zoneMorning != null)
                    z.zoneMorning.SetActive(true);
                break;
            case DayTime.AFTERNOON:
                foreach (GameObject item in currentZone.EnabledItemsAfternoon)
                {
                    if (item != null)
                        item.SetActive(false);
                }
                foreach (GameObject item in z.EnabledItemsAfternoon)
                {
                    if (item != null)
                        item.SetActive(true);
                }
                foreach (GameObject npc in currentZone.EnabledNPCsAfternoon)
                {
                    if (npc != null)
                        npc.SetActive(false);
                }
                foreach (GameObject npc in z.EnabledNPCsAfternoon)
                {
                    if (npc != null)
                        npc.SetActive(true);
                }
                if (z.zoneAfternoon != null)
                    z.zoneAfternoon.SetActive(true);
                break;
            case DayTime.NIGHT:
                foreach (GameObject item in currentZone.EnabledItemsNight)
                {
                    if (item != null)
                        item.SetActive(false);
                }
                foreach (GameObject item in z.EnabledItemsNight)
                {
                    if (item != null)
                        item.SetActive(true);
                }
                foreach (GameObject npc in currentZone.EnabledNPCsNight)
                {
                    if (npc != null)
                        npc.SetActive(false);
                }
                foreach (GameObject npc in z.EnabledNPCsNight)
                {
                    if (npc != null)
                        npc.SetActive(true);
                }
                if (z.zoneNight != null)
                    z.zoneNight.SetActive(true);
                break;
            default:
                break;
        }
        currentZone = z;
    }
    public void LoadStartingZone()
    {
        if(startingZoneIndx >=0 && startingZoneIndx < ZoneList.Length)
            ChangeZone(ZoneList[startingZoneIndx]);
    }
}
