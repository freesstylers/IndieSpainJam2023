using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor.SearchService;
#endif
using UnityEngine;

public class KeyHighlighting : MonoBehaviour
{
    public KeyCode key = KeyCode.LeftAlt;

    void Update()
    {
        if (Input.GetKeyDown(key))
            HighlightAll(true);
        else if (Input.GetKeyUp(key))
            HighlightAll(false);
    }

    void HighlightAll(bool state)
    {
        InteractableObject[] go = FindObjectsOfType<InteractableObject>();

        foreach (InteractableObject obj in go)
        {
            obj.ToggleHighlightKey(state);
        }
    }
}
