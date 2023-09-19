using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory: MonoBehaviour
{
 
    public int maxitems = 6;
    private Dictionary<string, int> items;

    private void Start()
    {
        items = new Dictionary<string, int>();
    }
    public void AddItem(string _newItemID, int _quantity)
    {
        if (items.ContainsKey(_newItemID)){
            items[_newItemID] += _quantity;
            Debug.Log("Added " + _newItemID);
        }
        else if (items.Count < maxitems)
        {
            items.Add(_newItemID, _quantity);
            Debug.Log("Added " + _newItemID);
        }
        //en else disparar opci�n de intercambiar un objeto por otro con alg�n manager que permite disparar eventos de di�logo
        //else
        //EventManager.NewDialogue(ReplaceItemEvent(_item)) //o algo as�
    }

    public void ReplaceItem(string _newItemID, int _newItemQuantity, string _replacedItemID)
    {
        items.Remove(_replacedItemID);
        items[_newItemID] = _newItemQuantity;
    }

    public void RemoveItem(string _itemID)
    {
        items.Remove(_itemID);
    }
}
