using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitUI : MonoBehaviour
{
    public Text IdText;
    void Start()
    {
        IdText.text = GameManager.Instance.UserID;
    }

    void Update()
    {
        
    }
}
