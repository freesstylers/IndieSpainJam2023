using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script de prueba para ver que funcione el inventario, los objetos se recoger�an a trav�s de un di�logo tras clickar en un punto concreto del escenario.
public class CogerShit : MonoBehaviour
{
    private void OnTriggerEnter(Collider item)
    {
        this.gameObject.GetComponent<Inventory>().AddItem(item.gameObject.GetComponent<PickUpItem>().item);
        item.gameObject.GetComponent<PickUpItem>().PickUp();
    }
}
