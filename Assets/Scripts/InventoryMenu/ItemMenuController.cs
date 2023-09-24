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
        //GameObject player = GameObject.FindGameObjectWithTag("Player");
        //canvasInventory = player.GetComponent<Inventory>();
        //canvasInventory.AddItem("goku");
        //canvasInventory.AddItem("vegeta");
    }

    private void OnEnable()
    {
        if (GameManager.Instance == null)
            return;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        canvasInventory = player.GetComponent<Inventory>();

        if (canvasInventory.items == null)
            return;
        ItemsData data = GameManager.Instance.itemsData;
        itemMenus = new List<GameObject>();
        foreach (KeyValuePair<string, int> item in canvasInventory.items)
        {
            GameObject gObj = Instantiate(itemMenuPref, transform);
            AutoBuildItemMenu a = gObj.GetComponent<AutoBuildItemMenu>();
            a.Fill(data.itemsList[item.Key]);
            itemMenus.Add(gObj);
            a.fi = FillFullItem;
        }        
    }

    private void OnDisable()
    {
        itemMenus?.ForEach(x => Destroy(x));
        itemMenus = null;
    }

    public void FillFullItem(string desc, string name, Sprite sprite)
    {
        itemDesc.text = desc;
        itemName.text = name;
        itemSprite.sprite = sprite;
    }

}
