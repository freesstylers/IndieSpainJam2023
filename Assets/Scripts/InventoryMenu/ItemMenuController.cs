using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMenuController : MonoBehaviour
{

    public TMPro.TextMeshProUGUI itemDesc;
    public UnityEngine.UI.Image itemSprite;
    

    [SerializeField]
    private GameObject itemMenuPref;
    [SerializeField]
    private Inventory canvasInventory;

    private void Start()
    { 

    }

    private void OnEnable()
    {
        GameObject gObj = Instantiate(itemMenuPref, transform);
        AutoBuildItemMenu a = gObj.GetComponent<AutoBuildItemMenu>();
        a.fi = FillFullItem;
    }


    void Update()
    {

    }

    public void FillFullItem(string desc, Sprite sprite)
    {
        itemDesc.text = desc;
        itemSprite.sprite = sprite;
    }

}
