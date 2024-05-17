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
        if (btnName == " Next")//����>�ü�>������ ��
        {
            charIndex++;
            charIndex = charIndex % Characters.Length;//%�� ������ �� �� ������ ��
        }
        if(btnName == "Prev")//����>������>�ü� ��
        {
            charIndex--;
            charIndex = charIndex % Characters.Length;
            charIndex = charIndex < 0 ? charIndex + Characters.Length: charIndex;
            //�ε����� 0���� ���� �� �ε����� ���������� ���ļ� ���
        }
        Debug.Log($"CherIndex :{charIndex}");
        Characters[charIndex].SetActive(true);//��ư ���� �� ĳ���� ����
    }
}
