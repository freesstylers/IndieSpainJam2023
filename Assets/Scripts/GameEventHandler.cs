using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static DialogueData;

public class GameEventHandler : MonoBehaviour
{

    [HideInInspector]
    public int timesTriggered = -1;

    [Serializable]
    public struct Events
    {
        public string branchName;
        [Header("")]
        public UnityEvent events;
    }

    [SerializeField]
    private List<Events> eventList;

    private Dictionary<string, UnityEvent> eventsDictionary = null;



    [SerializeField]
    private DialogueData data;



    public void TriggerEvent()
    {
        if (DialogueManager.instance_.container.activeInHierarchy)
            return;

        if (eventsDictionary == null)
        {
            eventsDictionary = new Dictionary<string, UnityEvent>();

            foreach (var d in eventList)
            {
                eventsDictionary[d.branchName] = d.events;
            }
        }

        DialogueManager.instance_.StartDialogue(data, eventsDictionary);
        timesTriggered++;
    }

    /// <summary>
    /// Despues de mirar si tienes los items, activa para poder saltar el evento success
    /// si no, salta el evento fail
    /// </summary>
    /// <param name="inv"> Inventario del jugador </param>
    /// <returns> true si tiene todos los items necesarios </returns>
    public bool CanTriggerEvent(Inventory inv)
    {
        bool hasAll = true;

        //if (inv == null)
        //    hasAll = false;

        //foreach (string item in nItems)
        //{
        //    hasAll = inv.CheckItem(item) && hasAll;
        //}

        //TriggerEvent(hasAll);

        return hasAll;
    }

    /// <summary>
    /// Despues de mirar si tienes los items, activa para poder saltar el evento success
    /// si no, salta el evento fail
    /// </summary>
    /// <param name="inv"> Inventario del jugador </param>
    /// <returns> true si tiene todos los items necesarios </returns>
    public bool CanTriggerEvent(List<string> lItems)
    {
        bool hasAll = true;

        //if (lItems == null)
        //    hasAll = false;

        //foreach (string item in nItems)
        //{
        //    hasAll = lItems.Contains(item) && hasAll;
        //}

        //TriggerEvent(hasAll);

        return hasAll;
    }
}
