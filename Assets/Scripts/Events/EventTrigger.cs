using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    public List<string> nItems;
    public KeyCode inputDown = KeyCode.Space;
    //evento fail
    //evento success

    [HideInInspector]
    public int timesTriggered = 0;

    [SerializeField]
    private bool canTrigger = false;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(inputDown))
        {
            if (canTrigger)
            {
                //Salte evento
                timesTriggered++;
                Debug.Log("Trigger event");
            }
            else
            {
                //evento de que no puede
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Inventory inv = other.GetComponent<Inventory>();
        canTrigger = CanTriggerEvent(inv);
    }

    /// <summary>
    /// Despues de mirar si tienes los items, activa para poder saltar el evento success
    /// si no, salta el evento fail
    /// </summary>
    /// <param name="inv"> Inventario del jugador </param>
    /// <returns> true si tiene todos los items necesarios </returns>
    public bool CanTriggerEvent(Inventory inv)
    {
        if (inv == null)
            return false;

        bool hasAll = true;
        foreach (string item in nItems)
        {
            hasAll = inv.CheckItem(item) && hasAll;
        }

        return hasAll;
    }
}
