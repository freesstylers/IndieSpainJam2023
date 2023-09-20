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

    private GameObject player;

    public float interactDistance = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
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

        player.GetComponent<Player.PlayerMovement>().SetMove(false);

        if (Vector3.Distance(this.gameObject.transform.position, player.transform.position) < interactDistance)
            canInteract = true;
    }

    private void OnMouseExit()
    {
        player.GetComponent<Player.PlayerMovement>().SetMove(true);

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

    public void OnMouseUp()
    {
        if(canInteract)
            InteractCallback();
    }

    public virtual void InteractCallback()
    {
        //Este es el padre, no deberia hacer nada, los hijos heredaran
    }
}
