using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextAsset itemInfoCSVAsset;
    public TextAsset itemDescriptionCSVAsset;
    public static GameManager Instance { get; private set; }

    public DialogueManager dialogueManager;

    public ItemsData itemsData;
    public ItemScriptableObject itemSprites;

    public enum ItemsInfoColumns {KEYNAME, KEYDESCRIPTION, COMBINABLE, ELEMENT1, ELEMENT2 }
    public enum Language {ES, EN, CAT, EUS, GAL, VAL};

    public Language currentLanguage;

    public Queue<string> dialogueHistory;
    public int dialogueHistoryMax;

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
            dialogueHistory = new Queue<string>();
        }
    }

    private void Start()
    {
        GenerateItemData();
    }

    public void AddToHistory(string key)
    {
        dialogueHistory.Enqueue(key);

        if(dialogueHistory.Count > dialogueHistoryMax)
            dialogueHistory.Dequeue();
    }

    void GenerateItemData()
    {
        Dictionary<string, List<string>> itemsInfodic = CSVReader.ReadCSV(itemInfoCSVAsset);

        Dictionary<string, List<string>> itemsDescdic = CSVReader.ReadCSV(itemDescriptionCSVAsset);

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
            it.sprite = itemSprites.GetDictionary()[itemID];

            if (currentItemEntry[(int)ItemsInfoColumns.COMBINABLE] == "1") it.combinable = true;
            else it.combinable = false;

            Debug.LogError("UN OBJETO QUE SE CRAFTEA? NOMBRE: " + itemID + "   COSAS " + itemsInfodic.ContainsKey(currentItemEntry[(int)ItemsInfoColumns.ELEMENT1]) + " Y TAMBIEN " + itemsInfodic.ContainsKey(currentItemEntry[(int)ItemsInfoColumns.ELEMENT2]));

            if (itemsInfodic.ContainsKey(currentItemEntry[(int)ItemsInfoColumns.ELEMENT1]) && itemsInfodic.ContainsKey(currentItemEntry[(int)ItemsInfoColumns.ELEMENT2]))
            {
                Debug.LogError("CREANDO CRAFTEABLE!!!");

                string element1 = currentItemEntry[(int)ItemsInfoColumns.ELEMENT1], element2 = currentItemEntry[(int)ItemsInfoColumns.ELEMENT2];

                if (!itemsData.combinationTable.ContainsKey(element1))
                    itemsData.combinationTable[element1] = new Dictionary<string, string>();

                if (!itemsData.combinationTable.ContainsKey(element2))
                    itemsData.combinationTable[element2] = new Dictionary<string, string>();

                itemsData.combinationTable[element1][element2] = itemID;
                itemsData.combinationTable[element2][element1] = itemID;
            }
            itemsData.itemsList.Add(itemID, it);
        }

    }

    public Inventory GetInventory()
    {
        return GetComponent<Inventory>();
    }
}
