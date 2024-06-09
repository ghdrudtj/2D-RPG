using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
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

    public Text IdText;

    [Header("Warning")]
    public GameObject WarningUI;
    public Text WarningMassageTxt;
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
        GameManager.Instance.CharacterName = Characters[charIndex].name;
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
        IdText.text = GameManager.Instance.UserID;
        WarningMassageExit();
    }

    private void SetPanelInfo()
    {
        Nametxt.text = CharacterInfos[charIndex].Name;
        Featuretxt.text = CharacterInfos[charIndex].Feature;
        Charimage.sprite = Characters[charIndex].GetComponent<SpriteRenderer>().sprite;
    }
    public void HomeBtn()
    {
        WarningUI.SetActive(true);
        WarningMassageTxt.text = "정말 HONE으로 가시겠습니까?";
    }
    public void ExitBtn()
    {
        WarningUI.SetActive(true);
        WarningMassageTxt.text = "정말 게임을 나가시겠습니까?";
    }
    void WarningMassageExit()
    {
        WarningUI.SetActive(false);
    }
}
