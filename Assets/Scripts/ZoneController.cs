using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneController : MonoBehaviour
{
    public Zone[] ZoneList;

    [SerializeField]
    Zone currentZone;

    [Serializable]
    public struct Zone
    {
        public GameObject gameObject;
        public GameObject[] ObjectsToEnableDay;
        public GameObject[] ObjectsToEnableNight;
        public GameObject[] ObjectsToDisableDay;
        public GameObject[] ObjectsToDisableNight;
    }

    public void ChangeZone(Zone z)
    {
        //Current zone
        currentZone.gameObject.SetActive(false);

        currentZone = z;

        if (true)
        {
            for (int x = 0; x < currentZone.ObjectsToEnableDay.Length; ++x)
            {
                if (currentZone.ObjectsToEnableDay[x] != null)
                    currentZone.ObjectsToEnableDay[x].SetActive(false);
            }

            for (int x = 0; x < currentZone.ObjectsToDisableDay.Length; ++x)
            {
                if (currentZone.ObjectsToDisableDay[x] != null)
                    currentZone.ObjectsToDisableDay[x].SetActive(true);
            }
        }
        else
        {
            for (int x = 0; x < currentZone.ObjectsToEnableNight.Length; ++x)
            {
                if (currentZone.ObjectsToEnableNight[x] != null)
                    currentZone.ObjectsToEnableNight[x].SetActive(false);
            }

            for (int x = 0; x < currentZone.ObjectsToDisableNight.Length; ++x)
            {
                if (currentZone.ObjectsToDisableNight[x] != null)
                    currentZone.ObjectsToDisableNight[x].SetActive(true);
            }
        }
    }
}
