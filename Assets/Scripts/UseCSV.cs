using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UseCSV : MonoBehaviour
{
    public string key;

    private void Awake()
    {
        GetComponent<TextMeshProUGUI>().text = DialogueKeyHandler.Instance.GetText(key);
    }
}
