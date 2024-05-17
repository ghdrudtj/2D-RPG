using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SelectCharacter : MonoBehaviour
{
    [Header("Intor")]
    public Text Nametxt;
    public Text Featuretxt;
    public Image Charimage;

    [Header("Character")]
    public GameObject[] Characters;
    private int charIndex = 0;

    public void SelectCharacterBtn(string btnName)
    {
        Characters[charIndex].SetActive(false);
        if (btnName == " Next")//전사>궁수>마법사 순
        {
            charIndex++;
            charIndex = charIndex % Characters.Length;//%는 나눗셈 할 때 나머지 값
        }
        if(btnName == "Prev")//전사>마법사>궁수 순
        {
            charIndex--;
            charIndex = charIndex % Characters.Length;
            charIndex = charIndex < 0 ? charIndex + Characters.Length: charIndex;
            //인덱스가 0보다 작을 때 인덱스와 나머지값을 합쳐서 계산
        }
        Debug.Log($"CherIndex :{charIndex}");
        Characters[charIndex].SetActive(true);//버튼 눌릴 때 캐릭터 변경
    }
}
