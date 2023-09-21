using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Object Image Data", menuName = "FreeStylers/Spain Indie Game Jam 23/Object Image Data")]

public class ObjectImages : ScriptableObject
{
    [Serializable]
    public struct ObjectImageData
    {
        [SerializeReference]
        public string itemKey;
        [SerializeReference]
        public Sprite sprite;
    }

    [SerializeField]
    private List<ObjectImageData> data;

    private Dictionary<string, Sprite> dataDictionary = null;

    public Dictionary<string, Sprite> GetImages()
    {
        if (dataDictionary == null)
        {
            dataDictionary = new Dictionary<string, Sprite>();

            foreach (var image in data)
            {
                dataDictionary[image.itemKey] = image.sprite;
            }
        }
        
        return dataDictionary;
    }
}
