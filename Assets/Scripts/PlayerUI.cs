using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI Instance;
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
        
        StartCoroutine(TimerCoroutine());
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
           
            if (currentTime >= BestTime)
            {
                currentTime = BestTime;
                PlayerPrefs.SetInt("BestTime", BestTime);
                PlayerPrefs.Save();
            }
            if (gameCount <= 0)
            {

                SceneManager.LoadScene("ExitScene");
            }
            GameCountTxt.text = $"�����մϴ�. ������ Ŭ���� �ϼ̽��ϴ�.\n {gameCount:F1}";
        }
    }



    private void display()
    {
        CoinText.text = "����: " + GameManager.Instance.Coin;
        CharacterImg.sprite = Player.GetComponent<SpriteRenderer>().sprite;
        HpSlider.value = GameManager.Instance.PlayerHP;

        MansterCountText.text= "���� ���� ��: "+ GameManager.Instance.monsterCount;
        AttackCountText.text = "���� ���ݷ�: " + GameManager.Instance.player.GetComponent<Character>().AttackObj.GetComponent<Attack>().AttackDamage;
        SpeedCountText.text= "���� �ӵ�: " + GameManager.Instance.player.GetComponent<Character>().Speed;
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

            Playtime.text = "Play Time: " + FormatTime((int)Time.time);
        }
    }
    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
