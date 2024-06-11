using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerUI : MonoBehaviour
{
    public Image CharacterImg;
    public Text IdText;

    public Slider HpSlider;

    private GameObject Player;
    public GameObject spawnPos;

    public  Text CoinText;
    public Text MansterCountText;
    public Text AttackCountText;
    public Text SpeedCountText;
        
    
    void Start()
    {
       
        IdText.text = GameManager.Instance.UserID;
        Player = GameManager.Instance.SpawnPlayer(spawnPos.transform);
    }

    void Update()
    {
        display();
    }

    private void display()
    {
        CoinText.text = "����: " + GameManager.Instance.Coin;
        CharacterImg.sprite = Player.GetComponent<SpriteRenderer>().sprite;
        HpSlider.value = GameManager.Instance.PlayerHP;
        MansterCountText.text= "���� ���� ��: "+ GameManager.Instance.monsterCount;
        AttackCountText.text= "���� ���ݷ�: "+ GameManager.Instance.AttackDamage;
        SpeedCountText.text= "���� �ӵ�: " + GameManager.Instance.Speed;
    }

}
