using UnityEngine;

public class Monstor : MonoBehaviour
{
    public float MonsterHP;
    public float MonsterDamage;
    public float MonsterExp;
    public float MonsterCount;  

    private float moveTime = 0f;
    private float TurnTime = 0;
    private bool isDie =false;

    public float MoveSpeed;
    public GameObject[] ItemObj;

    private Animator MonsterAnimator;
    public float PlayerHP;

    void Start()
    {
        GameManager.Instance.monsterCount++;
       
        MonsterAnimator=this.GetComponent<Animator>();
    }
    void Update()
    {
        MobsterMove();
    }
    private void MobsterMove()
    {
        if(isDie)return;

        moveTime += Time.deltaTime;

        if (moveTime <= TurnTime) 
        {
            this.transform.Translate(MoveSpeed*Time.deltaTime,0,0);
        }
        else
        {
            TurnTime =Random.Range(1,5);
            moveTime = 0;

            transform.Rotate(0,180,0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDie) return;

        if (collision.gameObject.tag == "Player")
        {
            MonsterAnimator.SetTrigger("Attack");
            GameManager.Instance.PlayerHP -= MonsterDamage;
            if (PlayerHP < 0) return;
            {
                CameraPos.Instance.PlayCameraShake();
            }
        }
        if (collision.gameObject.tag == "Attack")
        {
            MonsterAnimator.SetTrigger("Damage");
            MonsterHP-=collision.gameObject.GetComponent<Attack>().AttackDamage;
            
            if (MonsterHP <= 0)
            {
                MonstorDie();
            }
        }
    }
    private void MonstorDie()
    {
        isDie = true;

        MonsterAnimator.SetTrigger("Die");
        GameManager.Instance.PlayerExp += MonsterExp;

        GetComponent<Collider2D>().enabled = false;
        Invoke("CreateItem", 1.5f);
        Destroy(gameObject, 1.55f);
        GameManager.Instance.monsterCount --;
    }
    private void CreateItem()
    {
        int itemRandom = Random.Range(0, ItemObj.Length*2);
        if (itemRandom < ItemObj.Length)
        {
            Instantiate(ItemObj[itemRandom], new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        }
    }
}
