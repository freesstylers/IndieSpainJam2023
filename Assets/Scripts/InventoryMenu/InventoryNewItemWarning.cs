using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryNewItemWarning : MonoBehaviour
{
    public static InventoryNewItemWarning Instance;

    public Color alertColor;
    public Image img;
    public GameObject alert;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void NewItemAlert()
    {
        img.color = alertColor;
        alert.SetActive(true);
    }

    public void RESET()
    {
        alert.SetActive(false);
        img.color = Color.white;
    }
}
