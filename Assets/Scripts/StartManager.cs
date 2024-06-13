using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    [Header("Menbership")]
    public GameObject MembershipUI;
    public InputField MenberShipIDTxt;
    public InputField MenberShipPWTxt;
    public InputField MenberShipFindTxt;

    [Header("Login")]
    public GameObject LoginUI;
    public InputField LoginIDTxt;
    public InputField LoginPWTxt;

    [Header("Find")]
    public GameObject FindUI;
    public InputField FindTxt;

    [Header("ErrorMessage")]
    public GameObject ErrorUI;
    public Text ErrorMassageTxt;

    public void MemberShipBtn()//������(���̵�,�н�����,���ε�) �Է�
    {
        PlayerPrefs.SetString("ID", MenberShipIDTxt.text);
        PlayerPrefs.SetString("PW", MenberShipPWTxt.text);
        PlayerPrefs.SetString("FIND", MenberShipFindTxt.text);

        MembershipUI.SetActive(false);
       
    }
    public void LoginBth()
    {
        if(PlayerPrefs.GetString("ID") != LoginIDTxt.text)//���̵� ��ġ���� ���� ��
        {
            LoginUI.SetActive(false );
            ErrorUI.SetActive(true );
            ErrorMassageTxt.text = "���̵� ��ġ���� �ʽ��ϴ�.";
            Invoke("ErrorMessageExit", 3f);//3�ʵ� false�� ��ȯ
            return;
        }
        if (PlayerPrefs.GetString("PW") != LoginPWTxt.text)//��й�ȣ�� ��ġ���� ���� ��
        {
            LoginUI.SetActive(false);
            ErrorUI.SetActive(true);
            ErrorMassageTxt.text = "��й�ȣ�� ��ġ���� �ʽ��ϴ�.";
            Invoke("ErrorMessageExit", 3f);
            return;
        }
        SceneManager.LoadScene("SelectScene");// ��ġ �� �� �̵�
    }
    public void FindBtn()
    {
        FindUI.SetActive(false);
        ErrorUI.SetActive(true) ;
        if ((PlayerPrefs.GetString("FIND")) == FindTxt.text)//���ε尡 ��ġ �� ��
        {
            ErrorMassageTxt.text=$"ID : {PlayerPrefs.GetString("ID")}\nPW : {PlayerPrefs.GetString("PW")}";//\n�� �� �ٲ�
        }
        else//���ε尡 ��ġ���� ���� ��
        {
            ErrorMassageTxt.text = "�߸��� ��Ʈ�Դϴ�.";
        }
        Invoke("ErrorMessageExit", 3f);
    }
    void ErrorMassageExit()
    {
        ErrorUI.SetActive(false);//��ҿ��� false
    }

}
