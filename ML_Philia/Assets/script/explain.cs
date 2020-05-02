using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explain : MonoBehaviour
{
    public GameObject extext;

    public void displaytext()
    {
        if (extext.activeSelf == true)
        {
            extext.SetActive(false);
        }
        else
        {
            extext.SetActive(true);
        }
    }
}
