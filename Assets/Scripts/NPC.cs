using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPC : MonoBehaviour
{
    public GameObject DialougueUI;

    private GameObject playerObj;
    private float distance;

    public GameObject[] DialogueTextObj;
    private int dlndex = 0;
    void Start()
    {

    }

    void Update()
    {
        if (playerObj == null) playerObj = GameManager.Instance.player;
        if (playerObj == null) return;

        distance = Vector2.Distance(transform.position, playerObj.transform.position);
        Debug.Log($"distance:{distance}");

        if (distance <= 3)
            DialougueUI.SetActive(true);
        else
            DialougueUI.SetActive(false);
    }
    public void NextBtn(string name)
    {
        DialogueTextObj[dlndex].SetActive(false);
        if (name == "Next")
        {
            if (dlndex < DialogueTextObj.Length - 1) dlndex++;
        }
        else if (name == "Prev")
        {
            if (dlndex > 0) dlndex--;
        }
        DialogueTextObj[dlndex].SetActive(true);
    }
    public void TownBth()
    {
        SceneManager.LoadScene("TownScene");
    }
}
    



