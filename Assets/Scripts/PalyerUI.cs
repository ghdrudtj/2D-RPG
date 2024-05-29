using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PalyerUI : MonoBehaviour
{
    public Image CharacterImg;
    public Text IdText;

    public Slider HpSlider;

    private GameObject player;
    void Start()
    {
        IdText.text=GameManager.Instance.UserID;
        GameObject playerPrefab = Resources.Load<GameObject>("Characters/" + GameManager.Instance.ChatacterName);
        player=Instantiate(playerPrefab);
    }

    void Update()
    {
        display();
    }
    private void display()
    {
        CharacterImg.sprite=player.GetComponent<SpriteRenderer>().sprite;
        HpSlider.value=GameManager.Instance.PlayerHP;
    }
}
