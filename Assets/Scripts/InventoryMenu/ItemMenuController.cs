using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMenuController : MonoBehaviour
{

    public TMPro.TextMeshProUGUI itemDesc;
    public TMPro.TextMeshProUGUI itemName;
    public UnityEngine.UI.Image itemSprite;
    

    [SerializeField]
    private GameObject itemMenuPref;
    [SerializeField]
    private Inventory canvasInventory;

    private List<GameObject> itemMenus;

    private void Start()
    {

    }

    private void OnEnable()
    {
        //AddItems();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Player.PlayerMovement>().SetMove(false);
        RebuildItems();

    }

    private void OnDisable()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Player.PlayerMovement>().SetMove(true);
    }

    public void FillFullItem(string desc, string name, Sprite sprite)
    {
        itemDesc.text = desc;
        itemName.text = name;
        itemSprite.sprite = sprite;
    }

    public void RebuildItems()
    {
        itemMenus?.ForEach(x => Destroy(x));
        itemMenus = null;

        if (GameManager.Instance == null)
            return;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        canvasInventory = player.GetComponent<Inventory>(); //Pillar el inventario del gameManager???

        if (canvasInventory.items == null)
            return;
        ItemsData data = GameManager.Instance.itemsData;
        itemMenus = new List<GameObject>();
        foreach (KeyValuePair<string, int> item in canvasInventory.items)
        {
            GameObject gObj = Instantiate(itemMenuPref, transform);
            AutoBuildItemMenu a = gObj.GetComponent<AutoBuildItemMenu>();
            a.Fill(data.itemsList[item.Key], this);
            itemMenus.Add(gObj);
        }
    }

    public void AddItems(){
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        canvasInventory = player.GetComponent<Inventory>(); //Pillar el inventario del gameManager???
        canvasInventory.AddItem("vegeta");
        canvasInventory.AddItem("goku");
    }


    public void CombineItems(string item1, string item2)
    {
        canvasInventory.TryCombineItems(item1, item2);
        RebuildItems();
    }
}
