using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestTime : MonoBehaviour
{
    void Start()
    {
        GetComponent<Text>().text = "Bext Time: " + PlayerUI.BestTime;
    }

    void Update()
    {
        
    }
}
