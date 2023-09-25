using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static DialogueData;

public class GameEventHandler : MonoBehaviour
{
    public string overrideName;

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

    public Dictionary<String,String> muertosConTag;

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

        DialogueManager.instance_.StartDialogue(data, eventsDictionary, overrideName);
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

    public void DestroyCharacterObject(GameObject character)
    {
        Destroy(character);
    }

    public void KillCharacter(string name)
    {
        GameObject[] ch = GameObject.FindGameObjectsWithTag(name);
        foreach(var c in ch)
        {
            c.transform.GetChild(0).gameObject.SetActive(false);
            c.transform.GetChild(1).gameObject.SetActive(true);
        }

        GameManager.Instance.GetInventory().AddItem("GENOCIDA");
    }
}
