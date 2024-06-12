using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class curruttime : MonoBehaviour
{
    void Start()
    {
        GetComponent<Text>().text = "play time: " + PlayerUI.currentTime;
    }

    void Update()
    {
        
    }
}
