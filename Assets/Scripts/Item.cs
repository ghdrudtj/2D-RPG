using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (gameObject.tag == "Coin")
            {
                GameManager.Instance.Coin += 10;
                Destroy(gameObject);
            }
            else if (gameObject.tag == "HP")
            {
                GameManager.Instance.PlayerHP += 10;
                Destroy(gameObject);
            }
            else if(gameObject.tag == "AttackUP")
            {
                Attack.Instance.AttackDamage += 1;
                Destroy(gameObject);
            }
            else if(gameObject.tag == "SpeedUP")
            {
                Character.Instance.Speed += 1;
                Destroy(gameObject);
            }
        }   
    }
    void Start()
    {
       
    }

    void Update()
    {
        
    }
}
