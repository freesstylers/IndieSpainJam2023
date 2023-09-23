using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static DialogueData;

[CreateAssetMenu(fileName = "New Item", menuName = "FreeStylers/Spain Indie Game Jam 23/Create Item")]
public class ItemScriptableObject : ScriptableObject
{
    [Serializable]
    public struct Item
    {
        [SerializeReference]
        public string key;
        [SerializeReference]
        public Sprite sprite;
    }

    [SerializeField]
    private List<Item> items;

    private Dictionary<string, Sprite> itemsDictionary = null;

    public Dictionary<string, Sprite> GetDictionary()
    {
        if (itemsDictionary == null)
        {
            itemsDictionary = new Dictionary<string, Sprite>();

            foreach (var d in items)
            {
                itemsDictionary[d.key] = d.sprite;
            }
        }

        return itemsDictionary;
    }


}