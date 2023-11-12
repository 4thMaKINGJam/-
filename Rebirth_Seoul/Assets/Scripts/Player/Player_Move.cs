using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player_Move : MonoBehaviour
{
    public float speed; //ìºë¦­?°ì˜ ?¤í”¼??

    private Vector3 vector; //x, y, z

    //speed * walkCount = ?œë²ˆ???´ë™???½ì?
    public int walkCount;
    private int currentWalkCount; //1??ì¦ê??˜ì—¬ walkCountë§Œí¼ ?˜ë©´ ë°˜ë³µë¬¸íƒˆì¶?

    private bool canMove = true;

    //? ë‹ˆë©”ì´??
    private Animator animator;

    //ë²?ëª»ì??˜ê?ê²??˜ê¸°
    private BoxCollider2D boxCollider;
    public LayerMask layerMask; //?´ë–¤ ?ˆì´?´ì— ì¶©ëŒ?ˆëŠ”ì§€ ?ë‹¨
    
    private float curTime;
    public float coolTime = 0.5f;

    private Player_HpBar playerHpBar;




    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        // Player_HpBar ?´ë˜?¤ì˜ ?¸ìŠ¤?´ìŠ¤ë¥??»ìŒ
        playerHpBar = FindObjectOfType<Player_HpBar>();
    }

    //?œë²ˆ ë°©í–¥???„ë? ?Œë§ˆ????ì¹??€ì§ì´ê²??˜ê¸°
    IEnumerator MoveCouroutine()
    {
        while(Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            if (vector.x != 0)
                vector.y = 0;

            //? ë‹ˆë©”ì´??ë³€??(???…ë ¥ê°?-1/1??dirë¡?ë°›ê¸°)
            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            RaycastHit2D hit; //?œì‘ ì§€?ì—????ì§€?ê¹Œì§€ ?ˆì´?€ ???„ë‹¬?˜ë©´ null
            Vector2 start = transform.position; //?œì‘ ì§€?? ?„ì¬?„ì¹˜
            Vector2 end = start + new Vector2(vector.x * speed * walkCount, vector.y * speed * walkCount); //??ì§€?? ?´ë™?˜ê³ ???˜ëŠ” ê³?
            
            hit = Physics2D.Linecast(start, end, LayerMask.GetMask("NoPassing"));

            if (hit.transform != null && hit.transform.CompareTag("Clear"))
            {
                SceneManager.LoadScene("End_Clear"); // ´ÙÀ½ ¾À ÀÌ¸§À¸·Î º¯°æ
                yield break; // ¾À ÀüÈ¯ ÈÄ ÀÌµ¿ ÄÚ·çÆ¾ Á¾·á
            }

            if (hit.transform != null)
            {
                break;
            }
            


            animator.SetBool("Walking", true);

            //ê±·ê¸°
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
