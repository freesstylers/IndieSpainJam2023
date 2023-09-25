using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public void AddItem(string key)
    {
        GameManager.Instance.GetInventory().AddItem(key);
    }

    public void RemoveItem(string key)
    {
        GameManager.Instance.GetInventory().RemoveItem(key);
    }
}
