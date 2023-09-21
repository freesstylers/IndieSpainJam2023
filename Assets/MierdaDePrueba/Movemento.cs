using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movemento : MonoBehaviour
{
    CharacterController cc;
    Inventory inventory;
    public float speed = 10;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        inventory = GetComponent<Inventory>();
    }

    void Update()
    {
        float x, y;
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        cc.Move(new Vector3(x * Time.deltaTime * speed, 0f, y * Time.deltaTime * speed));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            inventory.TryCombineItems("goku", "vegeta");
        }
    }
}
