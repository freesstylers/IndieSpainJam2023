using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script de prueba para ver que funcione el inventario, los objetos se recogerían a través de un diálogo tras clickar en un punto concreto del escenario.
public class CogerShit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PickUpItem pickedItem = other.gameObject.GetComponent<PickUpItem>();
        this.gameObject.GetComponent<Inventory>().AddItem(pickedItem.itemID, pickedItem.quantity);
    }
}
