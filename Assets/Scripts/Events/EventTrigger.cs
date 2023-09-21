using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    public List<string> nItems;
    public KeyCode inputDown = KeyCode.Space;

    [HideInInspector]
    public int timesTriggered = -1;

    [SerializeField]
    private CharacterScriptableObject itemDialog;

    public void TriggerEvent(bool hasItems)
    {
        AudioClip soundEffect=null;
        if (itemDialog.characterSoundEffect != null)
            soundEffect = itemDialog.characterSoundEffect[UnityEngine.Random.Range(0, itemDialog.characterSoundEffect.Length)];

        if (hasItems)
        {
            //Salte evento
            DialogueManager.instance_.StartDialogue(itemDialog.dialogues[0], soundEffect);
            timesTriggered++;
            Debug.Log("Trigger event");
        }
        else
        {
            //evento de que no puede
            DialogueManager.instance_.StartDialogue(itemDialog.dialogues[1], itemDialog.characterSoundEffect[UnityEngine.Random.Range(0, itemDialog.characterSoundEffect.Length)]);
        }
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

        if (inv == null)
            hasAll = false;

        foreach (string item in nItems)
        {
            hasAll = inv.CheckItem(item) && hasAll;
        }

        TriggerEvent(hasAll);

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

        if (lItems == null)
            hasAll = false;

        foreach (string item in nItems)
        {
            hasAll = lItems.Contains(item) && hasAll;
        }

        TriggerEvent(hasAll);

        return hasAll;
    }
}
