using UnityEngine;
using static Define;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Player SelectedPlayer;
    public string UserID;

    public float PlayerHP;
    public float PlayerExp;
    public int Coin = 0 ;

    public int monsterCount;

    public GameObject player;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(Instance);
    }

    private void Start()
    {
        UserID = PlayerPrefs.GetString("ID");
        
        
    }
    
    public GameObject SpawnPlayer(Transform spawnPos)
    {
        GameObject playerPrefab = Resources.Load<GameObject>("Characters/" + SelectedPlayer.ToString());
        player = Instantiate(playerPrefab, spawnPos.position, spawnPos.rotation);
        return player;
    }
}
