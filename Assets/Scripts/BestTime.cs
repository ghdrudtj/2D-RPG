using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestTime : MonoBehaviour
{
    void Start()
    {
        float bestTime = PlayerPrefs.GetInt("BestTime");
        GetComponent<Text>().text = "Best Time: " + FormatTime(bestTime);
    }
    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void Update()
    {
        
    }
}
