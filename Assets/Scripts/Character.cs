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
    //������
    private void Move()
    {
        if(Input.GetKey(KeyCode.RightArrow)) //������ ���� �����̰� GetKey ���
        {
            transform.Translate(Vector3.right*Speed*Time.deltaTime);//��� ��ġ �̵�
            animator.SetBool("Move",true);//move�� true�� ��(����) �ִϸ��̼�Move ����
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
            animator.SetBool("Move",false);//�ƹ��͵� ������ �ʾ��� �� false��
        }
    }
    private void Flip()
    {
        faceRight = !faceRight;

        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
    //����
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
    //����
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
    //��ٸ����� ������
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
