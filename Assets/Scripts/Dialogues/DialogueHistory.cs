using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueHistory : MonoBehaviour
{
    public GameObject prefabHistory;

    private void OnEnable()
    {
        foreach(Transform o in transform)
        {
            Destroy(o.gameObject);
        }

        foreach(string d in GameManager.Instance.dialogueHistory)
        {
            GameObject g = Instantiate(prefabHistory, transform);

            g.GetComponentInChildren<TextMeshProUGUI>().text = d;
        }
    }
}
