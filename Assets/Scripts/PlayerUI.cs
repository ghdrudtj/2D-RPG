using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Image CharacterImg;
    public Text IdText;
    public Text LVText;

    public Slider HpSlider;
    public Slider MpSlider;
    public Slider ExpSlider;

    private GameObject Player;
    
    void Start()
    {
        IdText.text = GameManager.Instance.UserID;
        GameObject spawnPos = GameObject.FindGameObjectWithTag("initPos");
        Player = GameManager.Instance.SpawnPlayer(spawnPos.transform);
    }

    void Update()
    {
        display();
    }

    private void display()
    {
        CharacterImg.sprite = Player.GetComponent<SpriteRenderer>().sprite;
        HpSlider.value = GameManager.Instance.PlayerStat.HP;
        MpSlider.value = GameManager.Instance.PlayerStat.MP;
        ExpSlider.value = GameManager.Instance.PlayerStat.Exp;
        LVText.text = "Lv : "+ GameManager.Instance.PlayerStat.Level;
    }
}
