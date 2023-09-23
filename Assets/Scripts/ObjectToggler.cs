using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToggler : MonoBehaviour
{
    public void ToggleObject(GameObject go)
    {
        go.SetActive(!go.activeSelf);
    }
}
