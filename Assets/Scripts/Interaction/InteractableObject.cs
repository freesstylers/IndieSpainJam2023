using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObject : MonoBehaviour
{
    public bool canInteract;

    public Outline outline;

    // Start is called before the first frame update
    void Start()
    {
        outline = gameObject.GetComponent<Outline>();
    }

    // Update is called once per frame
    private void OnMouseOver()
    {
        TurnOn();
    }

    private void OnMouseExit()
    {
        TurnOff();
    }

    public void TurnOff()
    {
        //glowControl.TurnOff();
        canInteract = false;
    }

    public void TurnOn()
    {
        //glowControl.TurnOn();
        canInteract = true;
    }
}
