using UnityEngine;
using static Define;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Player SelectedPlayer;
    public string UserID;

    public float PlayerHP;
    public float PlayerMP;
    public float PlayerExp;
    public float PlayerDef;
    public int Coin;

    public GameObject player;

    public Character character
    {
        get { return player.GetComponent<Character>(); }
    }
    public Attack CharacterAttack
    {
        get { return character.AttackObj.GetComponent<Attack>(); }
    }
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

    void Update()
    {

    }
}
