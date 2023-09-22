using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueKeyHandler : MonoBehaviour
{
    public enum Lang { ES, EN };
    public static DialogueKeyHandler Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            //Singleton
            Instance = this;
            Instance.Load();
        }
    }

    public string dialogueCSVPath;

    Dictionary<string, List<string>> keyData;

    public Lang language = Lang.ES;

    public bool DEBUG_NO_KEYS = false;

    void Load()
    {
        keyData = new Dictionary<string, List<string>>(CSVReader.ReadCSV(dialogueCSVPath));
    }

    public string GetText(string key)
    {
        if (!DEBUG_NO_KEYS && keyData.ContainsKey(key))
            return keyData[key][((int)language)];
        else
        {
            Debug.LogError("SE ESTÁ ENVIANDO UNA KEY, NO EL TEXTO CORRECTO");
            return key;
        }
    }
}
