using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    private GameObject PlayerObj;
    void Start()
    {
        
    }

    void Update()
    {
        if (PlayerObj == null)
        {
            PlayerObj = GameObject.FindGameObjectWithTag("Player");
        }
        transform.position = new Vector3(PlayerObj.transform.position.x,0,-10);
    }
}
