using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory: MonoBehaviour
{
 
    public int maxitems = 6;
    private Dictionary<string, int> items;
    public ItemsData itemsData;
    private void Start()
    {
        items = new Dictionary<string, int>();
        
        itemsData = GameManager.Instance.itemsData;

        //Todo esto por probar, la itemsdata se deberia de generar en otro sitio leyendo del csv
        //itemsData = ItemsData.CreateInstance<ItemsData>();
        //itemsData.itemsList = new Dictionary<string, Item>();
        //itemsData.combinationTable = new Dictionary<string, Dictionary<string, string>>();
        //Item it = new Item();
        //it.name = "Cock";
        //it.id = "Poronga";
        //it.description = "Very nice cock";
        //itemsData.itemsList.Add("Poronga", it);
    }
    public void AddItem(string _newItemID, int _quantity)
    {
        if (!itemsData.itemsList.ContainsKey(_newItemID))
        {
            Debug.Log("Inexistent item ID: " + _newItemID);
            return;
        }
        if (items.ContainsKey(_newItemID)){
            items[_newItemID] += _quantity;
            Debug.Log("Added " + itemsData.itemsList[_newItemID].name);
        }
        else if (items.Count < maxitems)
        {
            items.Add(_newItemID, _quantity);
            Debug.Log("Added " + itemsData.itemsList[_newItemID].name);
        }
        //en else disparar opción de intercambiar un objeto por otro con algún manager que permite disparar eventos de diálogo
        //else
        //DialogueManager.NewDialogue(ReplaceItemEvent(_newItemID, _newItemQuantity, _replacedItemID)) //o algo así
    }

    public void ReplaceItem(string _newItemID, int _newItemQuantity, string _replacedItemID)
    {
        if (!itemsData.itemsList.ContainsKey(_newItemID))
        {
            Debug.Log("Inexistent item ID: " + _newItemID);
            return;
        }
        if (!itemsData.itemsList.ContainsKey(_replacedItemID))
        {
            Debug.Log("Inexistent item ID: " + _replacedItemID);
            return;
        }
        items.Remove(_replacedItemID);
        items[_newItemID] = _newItemQuantity;
    }

    public void RemoveItem(string _itemID)
    {
        if (!itemsData.itemsList.ContainsKey(_itemID))
        {
            Debug.Log("Inexistent item ID: " + _itemID);
            return;
        }
        items.Remove(_itemID);
    }

    public void TryCombineItems(string _itemID1, string _itemID2)
    {
        if (!itemsData.itemsList.ContainsKey(_itemID1))
        {
            Debug.Log("Inexistent item ID: " + _itemID1);
            return;
        }
        if (!itemsData.itemsList.ContainsKey(_itemID2))
        {
            Debug.Log("Inexistent item ID: " + _itemID2);
            return;
        }
        if (!itemsData.itemsList[_itemID1].combinable)
        {
            //DialogueManager.NewDialogue(ItemNotCombinable(_itemID1));
            Debug.Log("objeto " + itemsData.itemsList[_itemID1].name + "no combinable");
            return;
        }
        if (!itemsData.itemsList[_itemID2].combinable)
        {
            //DialogueManager.NewDialogue(ItemNotCombinable(_itemID2));
            Debug.Log("objeto " + itemsData.itemsList[_itemID2].name + "no combinable");
            return;
        }
        if (itemsData.combinationTable[_itemID1].ContainsKey(_itemID2))
        {
            RemoveItem(_itemID1);
            RemoveItem(_itemID2);
            AddItem(itemsData.combinationTable[_itemID1][_itemID2], 1);
        }
    }
        
    public bool CheckItem(string _itemID)
    {
        return items.ContainsKey(_itemID);
    }
}
