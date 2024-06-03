using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    private GameObject playerObj;
    void Start()
    {
        
    }

    void Update()
    {
        if (playerObj == null)
        {
            playerObj = GameObject.FindGameObjectWithTag("Player");
        }
        transform.position = new Vector3(playerObj.transform.position.x,0,-10);
    }
}
