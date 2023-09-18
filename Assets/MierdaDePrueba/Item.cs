using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//probablemente sea innecesario y a la información de los objetos se acceda con un archivo de texto externo
public class Item
{
    public int id;
    public string name;
    public string description;
    public Item(int _id, string _name, string _description)
    {
        this.id = _id;
        this.name = _name;
        this.description = _description;
    }
}
