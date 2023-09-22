using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string itemInfoCSVPath = "/CSVs/Files/ItemsInfoTest.csv";
    public string itemDescriptionCSVPath = "/CSVs/Files/ItemsDescriptionTest.csv";
    public static GameManager Instance { get; private set; }

    public DialogueManager dialogueManager;

    public ItemsData itemsData;

    public enum ItemsInfoColumns {KEYNAME, KEYDESCRIPTION, COMBINABLE, ELEMENT1, ELEMENT2 }
    public enum Language {ES, EN, CAT, EUS, GAL, VAL};

    public Language currentLanguage;

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

    void GenerateItemData()
    {

        Debug.Log("FALTA EL GENERAR OBJETOS!");

        return;


        Dictionary<string, List<string>> itemsInfodic = CSVReader.ReadCSV(itemInfoCSVPath);
        Dictionary<string, List<string>> itemsDescdic = CSVReader.ReadCSV(itemDescriptionCSVPath);

        itemsData = new ItemsData();

        foreach(string itemID in itemsInfodic.Keys)
        {
            Item it = new Item();
            it.id = itemID;

            List<string> currentItemEntry = itemsInfodic[itemID];

            string keyName;
            string keyDesc;

            keyName = currentItemEntry[(int)ItemsInfoColumns.KEYNAME];
            keyDesc = currentItemEntry[(int)ItemsInfoColumns.KEYDESCRIPTION];

            it.name = itemsDescdic[keyName][(int)currentLanguage];
            it.description = itemsDescdic[keyDesc][(int)currentLanguage];

            if (currentItemEntry[(int)ItemsInfoColumns.COMBINABLE] == "si") it.combinable = true;
            else it.combinable = false;

            if (itemsInfodic.ContainsKey(currentItemEntry[(int)ItemsInfoColumns.ELEMENT1]) && itemsInfodic.ContainsKey(currentItemEntry[(int)ItemsInfoColumns.ELEMENT2]))
            {
                string element1 = currentItemEntry[(int)ItemsInfoColumns.ELEMENT1], element2 = currentItemEntry[(int)ItemsInfoColumns.ELEMENT2];

                if (!itemsData.combinationTable.ContainsKey(element1))
                    itemsData.combinationTable.Add(element1, new Dictionary<string, string>());

                if (!itemsData.combinationTable.ContainsKey(element2))
                    itemsData.combinationTable.Add(element2, new Dictionary<string, string>());

                itemsData.combinationTable[element1].Add(element2, itemID);
                itemsData.combinationTable[element2].Add(element1, itemID);
            }
            itemsData.itemsList.Add(itemID, it);
        }

    }
}
