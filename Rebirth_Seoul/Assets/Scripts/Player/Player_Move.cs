using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player_Move : MonoBehaviour
{
    public float speed; //ĳ������ ���ǵ�

    private Vector3 vector; //x, y, z

    //speed * walkCount = �ѹ��� �̵��� �ȼ�
    public int walkCount;
    private int currentWalkCount; //1�� �����Ͽ� walkCount��ŭ �Ǹ� �ݺ���Ż��

    private bool canMove = true;

    //�ִϸ��̼�
    private Animator animator;

    //�� ���������� �ϱ�
    private BoxCollider2D boxCollider;
    public LayerMask layerMask; //� ���̾ �浹�ߴ��� �Ǵ�
    
    private float curTime;
    public float coolTime = 0.5f;

    private Player_HpBar playerHpBar;



    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        // Player_HpBar Ŭ������ �ν��Ͻ��� ����
        playerHpBar = FindObjectOfType<Player_HpBar>();
    }

    //�ѹ� ����Ű ���� ������ �� ĭ �����̰� �ϱ�
    IEnumerator MoveCouroutine()
    {
        while(Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            if (vector.x != 0)
                vector.y = 0;

            //�ִϸ��̼� ���� (Ű �Է°� -1/1�� dir�� �ޱ�)
            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            RaycastHit2D hit; //���� �������� �� �������� ������ �� �����ϸ� null
            Vector2 start = transform.position; //���� ����: ������ġ
            Vector2 end = start + new Vector2(vector.x * speed * walkCount, vector.y * speed * walkCount); //�� ����: �̵��ϰ��� �ϴ� ��

            boxCollider.enabled = false;
            hit = Physics2D.Linecast(start, end, LayerMask.GetMask("NoPassing"));
            boxCollider.enabled = true;

            if(hit.transform != null)
            {
                break;
            }

            animator.SetBool("Walking", true);

            //�ȱ�
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
        // �ڷ�ƾ �ܺο��� ���� �Է��� Ȯ��
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
