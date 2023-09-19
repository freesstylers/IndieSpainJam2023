using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObject : MonoBehaviour
{
    public bool canInteract;

    public Outline outline;

    public float MouseHighlightOutline;
    public float KeyHighlightOutline;

    public bool HighlightKeyOn = false;

    // Start is called before the first frame update
    void Start()
    {
        outline = gameObject.GetComponent<Outline>();
        outline.enabled = false;
    }

    // Update is called once per frame
    private void OnMouseOver()
    {
        if (!HighlightKeyOn)
            TurnOn();
        else
            outline.OutlineWidth = MouseHighlightOutline;
        canInteract = true;
    }

    private void OnMouseExit()
    {
        if (!HighlightKeyOn)
            TurnOff();
        else
            outline.OutlineWidth = KeyHighlightOutline;
        canInteract = false;
    }

    public void TurnOff()
    {
        outline.enabled = false;
    }

    public void TurnOn()
    {
        outline.enabled = true;
    }

    public void ToggleHighlightKey(bool state)
    {
        HighlightKeyOn = state;

        if (HighlightKeyOn)
        {
            outline.OutlineWidth = KeyHighlightOutline;
            TurnOn();
        }
        else
        {
            outline.OutlineWidth = MouseHighlightOutline;
            TurnOff();
        }
    }
}
