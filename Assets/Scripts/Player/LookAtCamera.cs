using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Camera camera;

    private void Start()
    {
        camera = FindAnyObjectByType<Camera>();
    }

    private void Update()
    {
        transform.rotation = camera.transform.rotation;
    }
}
