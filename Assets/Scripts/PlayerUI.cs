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

    public Text CoinText;
    public Text MansterCountText;
    public Text AttackCountText;
    public Text SpeedCountText;
    public Text Playtime;

    public Text GameCountTxt;
    public GameObject GameStart;

    public static int currentTime;
    public static int BestTime;

    private float gameCount = 3f;

    void Start()
    {
        IdText.text = GameManager.Instance.UserID;
        Player = GameManager.Instance.SpawnPlayer(spawnPos.transform);
        if(SceneManager.GetActiveScene().name == "MainScene")
        {
            StartCoroutine(TimerCoroutine());
        }
    }

    void Update()
    {
        display();
        CheckClear();
    }

    void CheckClear()
    {
        if(GameManager.Instance.monsterCount <= 0)
        {
            gameCount -= Time.deltaTime;
            if (gameCount <= 0)
            {
                
                SceneManager.LoadScene("ExitScene");
            }
            if (currentTime > BestTime)
            {
                currentTime = BestTime;
            }

            GameCountTxt.text = $"축하합니다. 게임을 클리어 하셨습니다.\n {gameCount:F1}";
        }
    }



    private void display()
    {
        CoinText.text = "코인: " + GameManager.Instance.Coin;
        CharacterImg.sprite = Player.GetComponent<SpriteRenderer>().sprite;
        HpSlider.value = GameManager.Instance.PlayerHP;

        MansterCountText.text= "남은 몬스터 수: "+ GameManager.Instance.monsterCount;
        AttackCountText.text = "현재 공격력: " + GameManager.Instance.player.GetComponent<Character>().AttackObj.GetComponent<Attack>().AttackDamage;
        SpeedCountText.text= "현재 속도: " + GameManager.Instance.player.GetComponent<Character>().Speed;
    }
    IEnumerator TimerCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            if (SceneManager.GetActiveScene().name != "MainScene")
            {
                StopCoroutine(TimerCoroutine());
                yield break;
            }

            Playtime.text = "Play Time: " + (int)Time.time;
        }
    }
}
