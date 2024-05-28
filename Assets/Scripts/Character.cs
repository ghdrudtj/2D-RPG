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
        if(Input.GetKey(KeyCode.RightArrow)) //������ ���� �����̰� GetKey ���
        {
            transform.Translate(Vector3.right*Speed*Time.deltaTime);//��� ��ġ �̵�
            animator.SetBool("Move",true);//move�� true�� ��(����) �ִϸ��̼�Move ����
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * Speed * Time.deltaTime);
            animator.SetBool("Move",true) ;
        }
        else
        {
            animator.SetBool("Move",false);//�ƹ��͵� ������ �ʾ��� �� false��
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))//������ ���� ���� ��ȯ�ϰ� GetKeyDown ���
        {
            spriteRenderer.flipX = false;
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            spriteRenderer.flipX = true;//���� ��ȯ ��
        }
    }
    private void Jump()
    {
        if (justJump)//JumpCheck���� true�� ��ȯ �Ǿ��� ��
        {
            justJump = false;//�ٽ� false�� ��ȯ
            
            rigidbody2d.AddForce(Vector2.up*JumpPower, ForceMode2D.Impulse);
            animator.SetTrigger("Jump");//��ġ ��ȯ �� �ִϸ��̼� ����
            audioSource.PlayOneShot(JumpClip); //�Ҹ� �߰� ����
        }
    }
    private void JumpCheck()
    {
        if (isFloor)//Floor�� ���� �� ��
        {
            if(Input.GetKeyDown(KeyCode.Space))//space�� ������ ��
            {
                justJump = true;//true�� ��ȯ
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
