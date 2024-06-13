using UnityEngine;

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
                GameManager.Instance.player.GetComponent<Character>().AttackObj.GetComponent<Attack>().AttackDamage += 5;
                Destroy(gameObject);
            }
            else if(gameObject.tag == "SpeedUP")
            {
                GameManager.Instance.player.GetComponent<Character>().Speed += 3;
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
