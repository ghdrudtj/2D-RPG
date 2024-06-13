using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] Text text;
    bool Active = false;
    [SerializeField] private Image image;
    private void Awake()
    {
        image.gameObject.SetActive(false);
    }

    public void textActive()
    {
        if (Active)
        {
            image.gameObject.SetActive (false);
            Active = false;
        }
        else
        {
            image.gameObject.SetActive (true);
            Active = true;
        }
        text.text = Active ? "X":"게임 설명";
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
