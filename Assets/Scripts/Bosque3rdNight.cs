using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bosque3rdNight : MonoBehaviour
{
    public GameObject Lepanto;

    private void OnEnable()
    {
        if (TimeManager.Instance.getCurrentDay() == 2)
        {
            Lepanto.SetActive(true);
            if (!GameManager.Instance.GetInventory().CheckItem("LANZALLAMAS") 
                && !GameManager.Instance.GetInventory().CheckItem("GENOCIDIO")
                && !GameManager.Instance.GetInventory().CheckItem("PATXARAN_OJOS"))
                Lepanto.SetActive(false);
        }
    }
}
