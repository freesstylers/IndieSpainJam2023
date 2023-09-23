using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory: MonoBehaviour
{
 
    public int maxitems = 6;
    private Dictionary<string, int> items;
    private List<string> selectedItems;
    private GameManager gm;
    public ItemsData itemsData;
    private void Start()
    {
        items = new Dictionary<string, int>();
        gm = GameManager.Instance;

        //Todo esto por probar, la itemsdata se deberia de generar en otro sitio leyendo del csv
        itemsData = new ItemsData();

        //Resources.Load<Sprite>("ItemSprites/test");

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
        if (!gm.itemsData.itemsList.ContainsKey(_newItemID))
        {
            Debug.Log("Inexistent item ID: " + _newItemID);
            return;
        }
        if (items.ContainsKey(_newItemID)){
            items[_newItemID] += _quantity;
            Debug.Log("Added " + gm.itemsData.itemsList[_newItemID].name);
        }
        else if (items.Count < maxitems)
        {
            items.Add(_newItemID, _quantity);
            Debug.Log("Added " + gm.itemsData.itemsList[_newItemID].name + "." + gm.itemsData.itemsList[_newItemID].description);
        }
        //en else disparar opción de intercambiar un objeto por otro con algún manager que permite disparar eventos de diálogo
        //else
        //DialogueManager.NewDialogue(ReplaceItemEvent(_newItemID, _newItemQuantity, _replacedItemID)) //o algo así
    }

    public void ReplaceItem(string _newItemID, int _newItemQuantity, string _replacedItemID)
    {
        if (!gm.itemsData.itemsList.ContainsKey(_newItemID))
        {
            Debug.Log("Inexistent item ID: " + _newItemID);
            return;
        }
        if (!gm.itemsData.itemsList.ContainsKey(_replacedItemID))
        {
            Debug.Log("Inexistent item ID: " + _replacedItemID);
            return;
        }
        items.Remove(_replacedItemID);
        items[_newItemID] = _newItemQuantity;
    }

    //elimina 1 unidad de ese objeto
    public void RemoveItem(string _itemID)
    {
        if (!gm.itemsData.itemsList.ContainsKey(_itemID))
        {
            Debug.Log("Inexistent item ID: " + _itemID);
            return;
        }
        items[_itemID]--;
        if(items[_itemID]<=0)
            items.Remove(_itemID);
    }

    public void TryCombineItems(string _itemID1, string _itemID2)
    {
        if (!gm.itemsData.itemsList.ContainsKey(_itemID1))
        {
            Debug.Log("Inexistent item ID: " + _itemID1);
            return;
        }
        if (!gm.itemsData.itemsList.ContainsKey(_itemID2))
        {
            Debug.Log("Inexistent item ID: " + _itemID2);
            return;
        }
        if (!gm.itemsData.itemsList[_itemID1].combinable)
        {
            //DialogueManager.NewDialogue(ItemNotCombinable(_itemID1));
            Debug.Log("objeto " + gm.itemsData.itemsList[_itemID1].name + "no combinable");
            return;
        }
        if (!gm.itemsData.itemsList[_itemID2].combinable)
        {
            //DialogueManager.NewDialogue(ItemNotCombinable(_itemID2));
            Debug.Log("objeto " + gm.itemsData.itemsList[_itemID2].name + "no combinable");
            return;
        }
        if (gm.itemsData.combinationTable[_itemID1].ContainsKey(_itemID2) && items.ContainsKey(_itemID1) && items.ContainsKey(_itemID2))
        {
            RemoveItem(_itemID1);
            RemoveItem(_itemID2);
            AddItem(gm.itemsData.combinationTable[_itemID1][_itemID2], 1);
        }
    }
        
    public bool CheckItem(string _itemID)
    {
        return items.ContainsKey(_itemID);
    }

    public bool CheckSelectedItems(List<string> needed)
    {

        bool isOk = true;

        if (needed.Count <= 0 && selectedItems.Count > 0)
            return isOk;

        if (needed == null || selectedItems == null || selectedItems.Count != needed.Count)
            return false;
        

        foreach (string item in needed)
        {
            isOk &= selectedItems.Contains(item);

            if (!isOk)
                break;
        }

        return isOk;
    }

    public bool SelectItem(string item)
    {
        if (!items.ContainsKey(item))
            return false;

        selectedItems.Add(item);

        return true;
    }

    public bool UnselectItem(string item)
    {
        return selectedItems.Remove(item);
    }
}
