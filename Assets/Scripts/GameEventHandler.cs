using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventHandler : MonoBehaviour
{

    [HideInInspector]
    public int timesTriggered = -1;

    [SerializeField]
    private DialogueData data;

    public void TriggerEvent()
    {
        if (DialogueManager.instance_.container.activeInHierarchy)
            return;

        DialogueManager.instance_.StartDialogue(data);
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
