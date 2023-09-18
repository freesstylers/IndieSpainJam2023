using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem: MonoBehaviour
{
    //lo suyo es que la info de objeto esté en un archivo externo, y los objetos recogibles al clickar sobre ellos lancen un diálogo que añada una referencia en el inventario a la información del objeto 
    //en caso de elegir la opción de recoger el objeto.
    public Item item;
    public void Start()
    {//y no esta verga
        item = new Item(0, "poronga", "bien venosa");
    }
    public void PickUp()
    {
        Debug.Log("Picked "+ item.name);
    }

}
