using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Item
{
    [SerializeField]
    public string id;
    [SerializeField]
    public string name;
    [SerializeField]
    public string description;
    [SerializeField]
    public bool combinable;
}
public class ItemsData
{
    public Dictionary<string, Item> itemsList;
    public Dictionary<string, Dictionary<string, string>> combinationTable;

    public ItemsData()
    {
        itemsList = new Dictionary<string, Item>();
        combinationTable = new Dictionary<string, Dictionary<string, string>>();
    }

}
