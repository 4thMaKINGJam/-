using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player_Move : MonoBehaviour
{
    public float speed; //캐릭?�의 ?�피??

    private Vector3 vector; //x, y, z

    //speed * walkCount = ?�번???�동???��?
    public int walkCount;
    private int currentWalkCount; //1??증�??�여 walkCount만큼 ?�면 반복문탈�?

    private bool canMove = true;

    //?�니메이??
    private Animator animator;

    //�?못�??��?�??�기
    private BoxCollider2D boxCollider;
    public LayerMask layerMask; //?�떤 ?�이?�에 충돌?�는지 ?�단
    
    private float curTime;
    public float coolTime = 0.5f;

    private Player_HpBar playerHpBar;




    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        // Player_HpBar ?�래?�의 ?�스?�스�??�음
        playerHpBar = FindObjectOfType<Player_HpBar>();
    }

    //?�번 방향???��? ?�마????�??�직이�??�기
    IEnumerator MoveCouroutine()
    {
        while(Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            if (vector.x != 0)
                vector.y = 0;

            //?�니메이??변??(???�력�?-1/1??dir�?받기)
            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            RaycastHit2D hit; //?�작 지?�에????지?�까지 ?�이?� ???�달?�면 null
            Vector2 start = transform.position; //?�작 지?? ?�재?�치
            Vector2 end = start + new Vector2(vector.x * speed * walkCount, vector.y * speed * walkCount); //??지?? ?�동?�고???�는 �?
            
            hit = Physics2D.Linecast(start, end, LayerMask.GetMask("NoPassing"));

            if (hit.transform != null && hit.transform.CompareTag("Clear"))
            {
                SceneManager.LoadScene("End_Clear"); // ���� �� �̸����� ����
                yield break; // �� ��ȯ �� �̵� �ڷ�ƾ ����
            }

            if (hit.transform != null)
            {
                break;
            }
            


            animator.SetBool("Walking", true);

            //걷기
            while (currentWalkCount < walkCount)
            {
                if (vector.x != 0)
                {
                    transform.Translate(vector.x * speed, 0, 0);
                }
                else if (vector.y != 0)
                {
                    transform.Translate(0, vector.y * speed, 0);
                }

                currentWalkCount++;
                yield return new WaitForSeconds(0.01f);
            }
            currentWalkCount = 0;

            
        }
        animator.SetBool("Walking", false);
        canMove = true;

    }

    

    // Update is called once per frame
    void Update()
    {

        if (curTime <= 0 && Input.GetKeyDown(KeyCode.Q))
        {
            if (vector.x != 0)
            {
                animator.SetTrigger("atk");
                if (vector.x < 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
            }
            else if(vector.y < 0)
            {
                animator.SetTrigger("atk_down");
            }
            else if (vector.y > 0)
            {
                animator.SetTrigger("atk_up");
            }
            curTime = coolTime;
        }
        else
        {
            curTime -= Time.deltaTime;
        }

        if (playerHpBar != null && playerHpBar.HpZero)
        {
            animator.SetBool("HpZero", true);
        }

    }

    void FixedUpdate() 
    {
        if (canMove)
        {
           
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)

            {
                canMove = false;
                StartCoroutine(MoveCouroutine());
            }
        }
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Clear"))
        {
            SceneManager.LoadScene("End_Clear");
        }
    }


}
