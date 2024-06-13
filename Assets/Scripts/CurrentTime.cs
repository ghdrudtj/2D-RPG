using UnityEngine;
using UnityEngine.UI;

public class CurrentTime : MonoBehaviour
{
    void Start()
    {
        GetComponent<Text>().text = "Play time: " + FormatTime(PlayerUI.currentTime);
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
