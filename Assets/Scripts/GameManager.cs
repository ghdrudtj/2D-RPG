using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string ChatacterName;
    public string UserID;

    public float PlayerHP;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if(Instance!=this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(Instance);
    }

    private void Start()
    {
        UserID = PlayerPrefs.GetString("ID");
    }

    void Update()
    {
        
    }
}
