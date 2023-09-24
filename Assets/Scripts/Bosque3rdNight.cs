using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bosque3rdNight : MonoBehaviour
{
    public GameObject Lepanto;

    private void OnEnable()
    {
        Lepanto.SetActive(true);
        if (TimeManager.Instance.getCurrentDay() == 2) 
        {
            if (!GameManager.Instance.GetInventory().CheckItem("LANZALLAMAS") && !GameManager.Instance.GetInventory().CheckItem("GENOCIDIO"))
                Lepanto.SetActive(false);
        }
    }
}
