using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public Text Playtime;

    public static int currentTime;

    void Start()
    {
       
        IdText.text = GameManager.Instance.UserID;
        Player = GameManager.Instance.SpawnPlayer(spawnPos.transform);
        StartCoroutine(TimerCoroutine());
    }

    void Update()
    {
        display();
    }

    private void display()
    {
        CoinText.text = "코인: " + GameManager.Instance.Coin;
        CharacterImg.sprite = Player.GetComponent<SpriteRenderer>().sprite;
        HpSlider.value = GameManager.Instance.PlayerHP;
        MansterCountText.text= "남은 몬스터 수: "+ GameManager.Instance.monsterCount;
        AttackCountText.text= "현재 공격력: "+ Attack.Instance.AttackDamage;
        SpeedCountText.text= "현재 속도: " + Character.Instance.Speed;
    }
    IEnumerator TimerCoroutine()
    {
        float currentTime = 0;
        
        currentTime += Time.deltaTime;
        if (GetComponent<Text>() != null)
            GetComponent<Text>().text = "Play Time: " + currentTime.ToString();
       
        else if (SceneManager.GetActiveScene().name != "MainScene")
        {
            yield break;
        }
    }
}
