using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [Header("Warning")]
    public GameObject WarningUI;
    public Text WarningMassageTxt;

    public GameObject Home_Yes;
    public GameObject Change_Yes;
    public GameObject Exit_Yes;

    private bool isHomeButtonClicked = false;
    private bool isExitButtonClicked = false;
    private bool isChangeButtonClicked = false;

    private float gameCount = 2f;
    public GameObject GameStart;
    public Text GameCountTxt;

    
    void Start()
    {
        WarningMassageExit();
    }



    public void HomeBtn()
    {
        WarningUI.SetActive(true);
        Home_Yes.SetActive(true);
        WarningMassageTxt.text = "Player�� ��� ������ �ʱ�ȭ �˴ϴ�. ���� HONE���� ���ðڽ��ϱ�?";
    }
    public void ExitBtn()
    {
        WarningUI.SetActive(true);
        Exit_Yes.SetActive(true);
        WarningMassageTxt.text = "Player�� ��� ������ �ʱ�ȭ �˴ϴ�. ���� ������ �����Ͻðڽ��ϱ�?";
    }
    public void ChangeBtn()
    {
        WarningUI.SetActive(true);
        Change_Yes.SetActive(true);
        WarningMassageTxt.text = "Player�� ��� ������ �ʱ�ȭ �˴ϴ�. ���� ������ �ٲٽðڽ��ϱ�?";
    }
    void WarningMassageExit()
    {
        WarningUI.SetActive(false);
    }
    private void Update()
    {
        if (isHomeButtonClicked)
        {
            gameCount -= Time.deltaTime;
            if (gameCount <= 0)
            {
                SceneManager.LoadScene("StartScene");
            }
            GameCountTxt.text = $"�� Ȩ���� �̵��˴ϴ�.\n {gameCount:F1}";
        }
        if (isExitButtonClicked)
        {
            gameCount -= Time.deltaTime;
            if (gameCount <= 0)
            {
                SceneManager.LoadScene("ExitScene");
            }
            GameCountTxt.text = $"�� ������ ����˴ϴ�.\n {gameCount:F1}";
        }
        if (isChangeButtonClicked)
        {
            gameCount -= Time.deltaTime;
            if (gameCount <= 0)
            {
                SceneManager.LoadScene("SelectScene");
            }
            GameCountTxt.text = $"�� ���� ȭ������ �̵��˴ϴ�.\n {gameCount:F1}";
        }
    }

    public void GameUIBtn(string btnName)
    {
        if (btnName == "Home_Yes")
        {
            WarningUI.SetActive(false);
            isHomeButtonClicked = true;
            GameStart.SetActive(true);
        }
        if (btnName == "Change_Yes")
        {
            WarningUI.SetActive(false);
            isChangeButtonClicked = true;
            GameStart.SetActive(true);
        }
        if (btnName == "Exit_Yes")
        {
            WarningUI.SetActive(false);
            isExitButtonClicked = true;
            GameStart.SetActive(true);
        }
    }
}
