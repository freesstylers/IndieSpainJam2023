using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem SaveSystemInstance;
    public Dictionary<string, object> saveDataDictionary;
    public string saveData { get; private set; } = "";
    public bool isReading, isWriting;
    public bool isDirty = false;

    public bool FirstLoadCompleted
    {
        get
        {
            return firstLoadCompleted;
        }

        set
        {
            firstLoadCompleted = value;
        }
    }

    bool allowSave
    {
        get
        {
            return true; //By default, we should be able to save
        }
    }

    private bool firstLoadCompleted = false;

    void Awake()
    {
        if (SaveSystemInstance == null) //Singleton system
        {
            DontDestroyOnLoad(this);
            SaveSystemInstance = this;

            EnsureInitialized();
        }
        else if (SaveSystemInstance != this)
            Destroy(this);
    }

    public void EnsureInitialized()
    {
        if (!initialized)
            Init();
    }

    public void UpdateDataToSave(string latestData)
    {
        if (latestData != saveData)
        {
            saveData = latestData;
            isDirty = true;
        }
    }

    public bool Read(out string saveFileText)
    {
        saveFileText = saveData;

        bool val = false;

        if (saveFileText == "")
            val = ReadInternal(out saveFileText);
        else
            val = true;

        if (val)
        {
            Dictionary<string, object> result = MiniJSON.Json.Deserialize(saveFileText) as Dictionary<string, object>;

            if (result != null)
            {
                if (saveDataDictionary != null)
                    saveDataDictionary.Clear();
                else
                    saveDataDictionary = new Dictionary<string, object>();

                foreach (KeyValuePair<string, object> entry in result)
                {
                    if (entry.Key == "")
                    {
                        //saveDataDictionary.Add(entry.Key, auxList);
                    }
                    else if (entry.Key == "")
                    {                     
                        //saveDataDictionary.Add(entry.Key, auxList);
                    }
                    else if (entry.Key == "")
                    {
                        //saveDataDictionary.Add(entry.Key, auxList);
                    }
                    if (entry.Key == "")
                    {
                       //saveDataDictionary.Add(entry.Key,);
                    }
                    if (entry.Key == "")
                    {
                       //saveDataDictionary.Add(entry.Key);
                    }
                }
            }
            else
                saveDataDictionary = new Dictionary<string, object>();
        }
        else
            saveDataDictionary = new Dictionary<string, object>();

        return val;
    }

    public bool Write()
    {
        if (!allowSave)
        {
            Debug.Log("user management is not allowing to save");
            return false;
        }

        string aux = MiniJSON.Json.Serialize(saveDataDictionary);
        UpdateDataToSave(aux);

        if (!isDirty || isWriting || isReading || !FirstLoadCompleted)
        {
            return false;
        }

        isWriting = true;
        isDirty = false;

        Debug.Log("IsWritting true");

        StopCoroutine(ShowSaveIcon());
        StartCoroutine(ShowSaveIcon());

        WriteInternal();

        return true;
    }

    public void AbortOperations()
    {
        isDirty = false;
        isReading = false;
        isWriting = false;

        SaveSystemInstance.FirstLoadCompleted = false;
    }

    IEnumerator ShowSaveIcon()
    {
        Debug.Log("SHOW SAVE ICON FUNCTION NOT YET IMPLEMENTED");
        yield break;
    }

    public bool initialized = false;

    private readonly string saveFileName = "IndieSpainJam23.dat";

    string savefilePath;

    public void Init()
    {
        savefilePath = Application.persistentDataPath + "/" + SaveSystem.SaveSystemInstance.saveFileName;

        Load();

        initialized = true;
    }

    public bool Load() //return success
    {
        bool goToEnd = false;

        if (File.Exists(savefilePath))
        {
            goToEnd = true;
        } //avoid trying to read the file if the save file doesn't exist yet

        if (goToEnd)
        {
            //filesize

            FileStream file = File.Open(savefilePath, FileMode.Open);
            var byteData = new byte[file.Length];

            file.Read(byteData, 0, (int)file.Length);

            file.Close();
            SaveSystem.SaveSystemInstance.UpdateDataToSave(System.Text.Encoding.UTF8.GetString(byteData));

            SaveSystem.SaveSystemInstance.FirstLoadCompleted = true;
        }
        else
        {
            SaveSystem.SaveSystemInstance.isReading = false;
            Debug.Log("load failed. could not find save data file");
            SaveSystem.SaveSystemInstance.FirstLoadCompleted = true;

            return false;
        }

        SaveSystem.SaveSystemInstance.isReading = false;

        return true;
    }

    public void WriteInternal()
    {
        var byteData = System.Text.Encoding.UTF8.GetBytes(SaveSystem.SaveSystemInstance.saveData);

        SaveSystem.SaveSystemInstance.isWriting = true;

        FileStream file = File.Create(savefilePath);
        file.Write(byteData);
        file.Close();

        SaveSystem.SaveSystemInstance.isWriting = false;
    }

    public bool ReadInternal(out string saveFileText)
    {
        saveFileText = SaveSystem.SaveSystemInstance.saveData;

        return SaveSystem.SaveSystemInstance.saveData != "";
    }
}