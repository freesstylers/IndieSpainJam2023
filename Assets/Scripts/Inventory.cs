using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory: MonoBehaviour
{
    public int maxitems = 6;
    private List<Item> items;

    private void Start()
    {
        items = new List<Item>();
    }
    public void AddItem(Item _newItem)
    {
        if(items.Count<maxitems)
            items.Add(_newItem);
        //en else disparar opci�n de intercambiar un objeto por otro con alg�n manager que permite disparar eventos de di�logo
        //else
        //EventManager.NewDialogue(ReplaceItemEvent(_item)) //o algo as�
    }

    public void ReplaceItem(Item _newItem, Item _replacedItem)
    {
        items.Remove(_replacedItem);
        items.Add(_newItem);
    }

    public void RemoveItem(Item _item)
    {
        items.Remove(_item);
    }
}
