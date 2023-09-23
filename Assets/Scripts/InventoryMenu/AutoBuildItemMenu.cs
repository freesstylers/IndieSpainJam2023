using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoBuildItemMenu : MonoBehaviour
{

    //Scriptable con el objeto con la info
    public delegate void FillItem(string desc, Sprite sprite);
    public FillItem fi;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fill()
    {

    }

    public void SendInfo()
    {
        fi.Invoke("Puto", null);
    }

}
