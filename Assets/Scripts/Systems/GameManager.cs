using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public DialogueManager dialogueManager;

    public ItemsData itemsData;

    void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(this.gameObject);
        }
        else
        {
            //Singleton
            Instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        GenerateItemData();
    }

    void GenerateItemData(){
        //provisional esto no va a leerse de la misma manera
        Dictionary<string, List<string>> dic = CSVReader.ReadCSV("/Editor/CSVs/Files/Test.csv");
        itemsData = ItemsData.CreateInstance<ItemsData>();

        foreach(string itemID in dic.Keys)
        {
            Item it;
            it.id = itemID;
            it.name = dic[itemID][0];
            it.description = dic[itemID][1];
            if (dic[itemID][2] == "true") it.combinable = true;
            else it.combinable = false;
            if(dic[itemID][3] != "")
            {
                itemsData.combinationTable[dic[itemID][3]][dic[itemID][4]] = itemID;
                itemsData.combinationTable[dic[itemID][4]][dic[itemID][3]] = itemID;
            }
            itemsData.itemsList.Add(itemID, it);
        }

    }
}
