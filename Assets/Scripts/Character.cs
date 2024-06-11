using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Character : MonoBehaviour
{
    public static Character Instance;

    private Animator animator;
    private Rigidbody2D rigidbody2d; 
    private AudioSource audioSource;

    public AudioClip JumpClip;

    public int Speed = 5;
    public float JumpPower;

    private bool isFloor;
    private bool isLadder;
    private bool isClimbing;
    private float InputVertical;

    public GameObject AttackObj;
    public float AttackSpeed;
    public AudioClip AttackClip;

    private bool justAttack,justJump;
    private bool faceRight =true;
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Move();
        JumpCheck();
        AttackCheck();
        ClimbingChack();
    }
    private void FixedUpdate()
    {
        Jump();
        Attack();
        Cilbing();
    }
    //움직임
    private void Move()
    {
        if(Input.GetKey(KeyCode.RightArrow)) //눌리는 동안 움직이게 GetKey 사용
        {
            transform.Translate(Vector3.right*Speed*Time.deltaTime);//상대 위치 이동
            animator.SetBool("Move",true);//move가 true일 때(동안) 애니메이션Move 실행
            if (!faceRight) Flip();
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * Speed * Time.deltaTime);
            animator.SetBool("Move",true) ;
            if (faceRight) Flip();
        }
        else
        {
            animator.SetBool("Move",false);//아무것도 눌리지 않았을 때 false로
        }
    }
    private void Flip()
    {
        faceRight = !faceRight;

        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
    //점프
    private void Jump()
    {
        if (justJump)//JumpCheck에서 true로 전환 되었을 때
        {
            justJump = false;//다시 false로 전환
            
            rigidbody2d.AddForce(Vector2.up*JumpPower, ForceMode2D.Impulse);
            animator.SetTrigger("Jump");//위치 변환 및 애니메이션 실행
            audioSource.PlayOneShot(JumpClip); //소리 추가 실행
        }
    }
    private void JumpCheck()
    {
        if (isFloor)//Floor가 감지 될 때
        {
            if(Input.GetKeyDown(KeyCode.Space))//space를 눌렸을 때
            {
                justJump = true;//true로 전환
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isFloor= true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isFloor= false;
        }
    }
    //공격
    private void AttackCheck()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            justAttack = true;
        }
    }
    private void Attack()
    {
        if (justAttack)
        {
            justAttack = false;

            animator.SetTrigger("Attack");
            audioSource.PlayOneShot(AttackClip);

            if (gameObject.name == "Warrior(Clone)")
            {
                AttackObj.GetComponent<Collider2D>().enabled = true;
                Invoke("SetAttackObjInactive", 0.5f);
            }
            else
            {
                if (!faceRight)
                {
                    GameObject obj = Instantiate(AttackObj, transform.position, Quaternion.Euler(0, 180f, 0));
                    obj.GetComponent<Rigidbody2D>().AddForce(Vector2.left * AttackSpeed, ForceMode2D.Impulse);
                    Destroy(obj, 3f);
                }
                else
                {
                    GameObject obj = Instantiate(AttackObj, transform.position, Quaternion.Euler(0, 0, 0));
                    obj.GetComponent<Rigidbody2D>().AddForce(Vector2.right * AttackSpeed, ForceMode2D.Impulse);
                    Destroy(obj, 3f);
                }
            }
        }
    }
    private void SetAttackObjInactive()
    {
        AttackObj.GetComponent<Collider2D>().enabled = false;
    }
    //사다리에서 움직임
    private void ClimbingChack()
    {
        InputVertical = Input.GetAxis("Vertical");
        if(isLadder && Mathf.Abs(InputVertical) > 0)
        {
            isClimbing = true;
        }
    }
    private void Cilbing()
    {
        if(isClimbing)
        {
            rigidbody2d.gravityScale = 0f;
            rigidbody2d.velocity=new Vector2(rigidbody2d.velocity.x,InputVertical*Speed);
        }
        else
        {
            rigidbody2d.gravityScale=1f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ladder")
        {
            isLadder = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ladder")
        {
            isClimbing = false;
            isLadder=false;
        }
    }
}
