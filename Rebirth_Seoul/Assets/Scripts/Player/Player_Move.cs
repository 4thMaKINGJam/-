using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player_Move : MonoBehaviour
{
    public float speed; //캐릭터의 스피드

    private Vector3 vector; //x, y, z

    //speed * walkCount = 한번에 이동할 픽셀
    public int walkCount;
    private int currentWalkCount; //1씩 증가하여 walkCount만큼 되면 반복문탈출

    private bool canMove = true;

    //애니메이션
    private Animator animator;

    //벽 못지나가게 하기
    private BoxCollider2D boxCollider;
    public LayerMask layerMask; //어떤 레이어에 충돌했는지 판단
    
    private float curTime;
    public float coolTime = 0.5f;

    private Player_HpBar playerHpBar;



    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        // Player_HpBar 클래스의 인스턴스를 얻음
        playerHpBar = FindObjectOfType<Player_HpBar>();
    }

    //한번 방향키 누를 때마다 한 칸 움직이게 하기
    IEnumerator MoveCouroutine()
    {
        while(Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            if (vector.x != 0)
                vector.y = 0;

            //애니메이션 변수 (키 입력값 -1/1을 dir로 받기)
            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            RaycastHit2D hit; //시작 지점에서 끝 지점까지 레이저 잘 도달하면 null
            Vector2 start = transform.position; //시작 지점: 현재위치
            Vector2 end = start + new Vector2(vector.x * speed * walkCount, vector.y * speed * walkCount); //끝 지점: 이동하고자 하는 곳

            boxCollider.enabled = false;
            hit = Physics2D.Linecast(start, end, LayerMask.GetMask("NoPassing"));
            boxCollider.enabled = true;

            if(hit.transform != null)
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
        // 코루틴 외부에서 공격 입력을 확인
        if (curTime <= 0 && Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetBool("Walking", false);
            animator.SetTrigger("atk");

            curTime = coolTime;
        }
        else
        {
            curTime -= Time.deltaTime;
        }

        if(playerHpBar != null && playerHpBar.HpZero)
        {
            animator.SetBool("HpZero", true);
        }

    }

    void FixedUpdate() 
    {
        if (canMove)
        {
            if ((Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0))
            {
                canMove = false;
                StartCoroutine(MoveCouroutine());
            }

        }

    }


}
