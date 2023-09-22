using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem SaveSystemInstance;
    public Dictionary<string, object> saveDataDictionary;
    public string saveData { get; private set; } = "Test.dat";
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

            Load();
        }
        else if (SaveSystemInstance != this)
            Destroy(this);
    }

    public bool Load() //return success
    {
        bool ret = false;

        int aux = PlayerPrefs.GetInt("Pelayo");

        Debug.LogError(aux);

        return true;
    }

    public void WriteInternal()
    {
        PlayerPrefs.SetInt("Pelayo", 1);

        Debug.LogError("Written");
    }
}