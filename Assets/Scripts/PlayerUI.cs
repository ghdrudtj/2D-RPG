using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    private float gameCount = 2f;

    bool timeActive = false;
    void Start()
    {
        IdText.text = GameManager.Instance.UserID;
        Player = GameManager.Instance.SpawnPlayer(spawnPos.transform);
        
        StartCoroutine(TimerCoroutine(currentTime));
        currentTime = 0;

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
            GameStart.SetActive(true);
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
     IEnumerator TimerCoroutine(int currentTime)
    {

        while (true)
        {
            Playtime.text = "Play Time: " + FormatTime(currentTime);
            yield return new WaitForSeconds(1); 
            currentTime++;
           
            
           if (PlayerUI.currentTime >= PlayerUI.BestTime)
           {
              PlayerUI.BestTime = PlayerUI.currentTime;
              yield return null;
           }
            
        }
    }
    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
