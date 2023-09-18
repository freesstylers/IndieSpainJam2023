using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movemento : MonoBehaviour
{
    CharacterController cc;
    public float speed = 10;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        float x, y;
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        cc.Move(new Vector3(x * Time.deltaTime * speed, 0f, y * Time.deltaTime * speed));
    }
}
