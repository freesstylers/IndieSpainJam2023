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
        //en else disparar opción de intercambiar un objeto por otro con algún manager que permite disparar eventos de diálogo
        //else
        //EventManager.NewDialogue(ReplaceItemEvent(_item)) //o algo así
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
