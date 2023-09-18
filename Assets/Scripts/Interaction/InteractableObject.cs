using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool canInteract;

    // Start is called before the first frame update
    void Start()
    {
        //glowControl = gameObject.GetComponent<GlowObjectCmd>();
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
