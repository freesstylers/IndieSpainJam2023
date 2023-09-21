using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideFloor : MonoBehaviour
{
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }
}
