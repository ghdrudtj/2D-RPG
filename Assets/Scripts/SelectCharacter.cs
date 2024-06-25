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
            GameCountTxt.text = $"�� ������ ���۵˴ϴ�.\n {gameCount:F1}";
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
        if (btnName == "Next")//����>�ü�>������ ��
        {
            charIndex++;
            charIndex = charIndex % Characters.Length;//%�� ������ �� �� ������ ��
        }
        if (btnName == "Prev")//����>������>�ü� ��
        {
            charIndex--;
            charIndex = charIndex % Characters.Length;
            charIndex = charIndex < 0 ? charIndex + Characters.Length : charIndex;
            //�ε����� 0���� ���� �� �ε����� ���������� ���ļ� ���
        }
        Debug.Log($"CherIndex :{charIndex}");

        Characters[charIndex].SetActive(true);//��ư ���� �� ĳ���� ����

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
