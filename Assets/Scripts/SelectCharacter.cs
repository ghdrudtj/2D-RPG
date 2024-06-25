using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SelectCharacter : MonoBehaviour
{
    [Header("Into")]
    public Text Nametxt;
    public Text Featuretxt;
    public Image Charimage;

    [Header("Character")]
    public GameObject[] Characters;
    public CharacterInfo[] CharacterInfos;
    private int charIndex = 0;

    [Header("GameStart")]
    public GameObject GameStart;
    public Text GameCountTxt;
    private bool isPlayButtonClicked=false;
    private float gameCount = 3f;

    private void Update()
    {
        if (isPlayButtonClicked)
        {
            gameCount-=Time.deltaTime;
            if(gameCount<=0)
            {
                SceneManager.LoadScene("MainScene");
            }
            GameCountTxt.text = $"곧 게임이 시작됩니다.\n {gameCount:F1}";
        }
    }
    public void PlayBtn()
    {
        GameStart.SetActive(true);
        isPlayButtonClicked = true;
        Define.Player player = (Define.Player)Enum.Parse(typeof(Define.Player), Characters[charIndex].name);
        GameManager.Instance.SelectedPlayer = player;
    }

    public void SelectCharacterBtn(string btnName)
    {
        Characters[charIndex].SetActive(false);
        if (btnName == "Next")//전사>궁수>마법사 순
        {
            charIndex++;
            charIndex = charIndex % Characters.Length;//%는 나눗셈 할 때 나머지 값
        }
        if (btnName == "Prev")//전사>마법사>궁수 순
        {
            charIndex--;
            charIndex = charIndex % Characters.Length;
            charIndex = charIndex < 0 ? charIndex + Characters.Length : charIndex;
            //인덱스가 0보다 작을 때 인덱스와 나머지값을 합쳐서 계산
        }
        Debug.Log($"CherIndex :{charIndex}");

        Characters[charIndex].SetActive(true);//버튼 눌릴 때 캐릭터 변경

        SetPanelInfo();
    }
    public void Start()
    {
        SetPanelInfo();
    }

    private void SetPanelInfo()
    {
        Nametxt.text = CharacterInfos[charIndex].Name;
        Featuretxt.text = CharacterInfos[charIndex].Feature;
        Charimage.sprite = Characters[charIndex].GetComponent<SpriteRenderer>().sprite;
    }
}
