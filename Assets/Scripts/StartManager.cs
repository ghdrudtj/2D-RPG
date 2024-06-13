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

    public void MemberShipBtn()//데이터(아이디,패스워드,파인드) 입력
    {
        PlayerPrefs.SetString("ID", MenberShipIDTxt.text);
        PlayerPrefs.SetString("PW", MenberShipPWTxt.text);
        PlayerPrefs.SetString("FIND", MenberShipFindTxt.text);

        MembershipUI.SetActive(false);
       
    }
    public void LoginBth()
    {
        if(PlayerPrefs.GetString("ID") != LoginIDTxt.text)//아이디가 일치하지 않을 때
        {
            LoginUI.SetActive(false );
            ErrorUI.SetActive(true );
            ErrorMassageTxt.text = "아이디가 일치하지 않습니다.";
            Invoke("ErrorMessageExit", 3f);//3초뒤 false로 전환
            return;
        }
        if (PlayerPrefs.GetString("PW") != LoginPWTxt.text)//비밀번호가 일치하지 않을 때
        {
            LoginUI.SetActive(false);
            ErrorUI.SetActive(true);
            ErrorMassageTxt.text = "비밀번호가 일치하지 않습니다.";
            Invoke("ErrorMessageExit", 3f);
            return;
        }
        SceneManager.LoadScene("SelectScene");// 일치 시 씬 이동
    }
    public void FindBtn()
    {
        FindUI.SetActive(false);
        ErrorUI.SetActive(true) ;
        if ((PlayerPrefs.GetString("FIND")) == FindTxt.text)//파인드가 일치 할 때
        {
            ErrorMassageTxt.text=$"ID : {PlayerPrefs.GetString("ID")}\nPW : {PlayerPrefs.GetString("PW")}";//\n는 줄 바꿈
        }
        else//파인드가 일치하지 않을 때
        {
            ErrorMassageTxt.text = "잘못된 힌트입니다.";
        }
        Invoke("ErrorMessageExit", 3f);
    }
    void ErrorMassageExit()
    {
        ErrorUI.SetActive(false);//평소에는 false
    }

}
