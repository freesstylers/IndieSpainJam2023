using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor.SearchService;
#endif
using UnityEngine;

public class KeyHighlighting : MonoBehaviour
{
    KeyCode key = KeyCode.LeftShift;
    
    bool highlight = false;

    void Update()
    {
        if (!highlight && Input.GetKeyDown(key))
            HighlightAll(true);

        if (highlight && Input.GetKeyUp(key))
            HighlightAll(false);

        if (Input.GetKeyDown(key))
            Debug.Log("PRESS");

        if (highlight && Input.GetKeyUp(key))
            Debug.Log("UNPRESS");
    }

    void HighlightAll(bool state)
    {
        highlight = state;

        InteractableObject[] go = FindObjectsOfType<InteractableObject>();

        foreach (InteractableObject obj in go)
        {
            obj.ToggleHighlightKey(state);
        }
    }
}
