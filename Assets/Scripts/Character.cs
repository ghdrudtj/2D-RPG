using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Character : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody2d; 
    private AudioSource audioSource;

    public AudioClip JumpClip;

    public float Speed;
    public float JumpPower;

    private bool isFloor;

    public GameObject AttackObj;
    public float AttackSpeed;
    public AudioClip AttackClip;

    private bool justAttack,justJump;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Move();
        JumpCheck();
        AttackCheck();
    }
    private void FixedUpdate()
    {
        Jump();
        Attack();
    }
    private void Move()
    {
        if(Input.GetKey(KeyCode.RightArrow)) //눌리는 동안 움직이게 GetKey 사용
        {
            transform.Translate(Vector3.right*Speed*Time.deltaTime);//상대 위치 이동
            animator.SetBool("Move",true);//move가 true일 때(동안) 애니메이션Move 실행
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * Speed * Time.deltaTime);
            animator.SetBool("Move",true) ;
        }
        else
        {
            animator.SetBool("Move",false);//아무것도 눌리지 않았을 때 false로
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))//눌렸을 때만 방향 전환하게 GetKeyDown 사용
        {
            spriteRenderer.flipX = false;
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            spriteRenderer.flipX = true;//방향 전환 됨
        }
    }
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
            if (gameObject.name == "Warrior")
            {
                AttackObj.SetActive(true);
            }
            else
            {
                if (spriteRenderer.flipX)
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
        AttackObj.SetActive(false);
    }
}
