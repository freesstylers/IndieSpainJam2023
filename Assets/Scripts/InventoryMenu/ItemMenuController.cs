using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMenuController : MonoBehaviour
{

    public TMPro.TextMeshProUGUI itemDesc;
    public TMPro.TextMeshProUGUI itemName;
    public UnityEngine.UI.Image itemSprite;
    public Sprite basicSprite;
    

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
        GameObject player = DialogueManager.instance_.player;
        player.GetComponent<Player.PlayerMovement>().SetInteracting(true);
        RebuildItems();
    }

    private void OnDisable()
    {
        GameObject player = DialogueManager.instance_.player;
        player.GetComponent<Player.PlayerMovement>().SetInteracting(false);
    }

    public void FillFullItem(string desc, string name, Sprite sprite)
    {
        itemDesc.text = desc;
        itemName.text = name;
        itemSprite.sprite = sprite;

        if (sprite == null)
            itemSprite.sprite = basicSprite;
    }

    public void RebuildItems()
    {
        itemMenus?.ForEach(x => Destroy(x));
        itemMenus = null;

        if (GameManager.Instance == null)
            return;

        canvasInventory = GameManager.Instance.GetInventory(); //Pillar el inventario del gameManager???

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
        canvasInventory = GameManager.Instance.GetInventory();
        canvasInventory.AddItem("vegeta");
        canvasInventory.AddItem("goku");
    }


    public void CombineItems(string item1, string item2)
    {
        canvasInventory.TryCombineItems(item1, item2);
        FillFullItem("", "", null); //Limpiar si ha objeto mostrando
        RebuildItems();
    }

    public void ClearSelection()
    {
        FillFullItem("", "", null);
    }
}
