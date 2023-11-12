using UnityEngine;
using UnityEngine.UI;

public class Ghost : MonoBehaviour
{
    public Transform[] waypoints; // empty 배열의 위치를 저장할 배열
    public float moveSpeed = 5f; // 이동 속도
    public int maxHealth = 100;   // 최대 체력
    public int currentHealth;     // 현재 체력

    public int currentWaypointIndex = 0;
    private ghostHealthBar healthBar;
    private Animator animator;

    public AudioSource deathSound;

    void Start()
    {
        currentHealth = maxHealth; // 최대 체력으로 시작
        healthBar = GetComponentInChildren<ghostHealthBar>();

        animator = GetComponent<Animator>();

        if (healthBar != null)
        {
            healthBar.Set_MaxHealth(maxHealth);
        }
        animator.SetBool("Right", true);


    }

    void Update()
    {
        if (currentHealth > 0)
        {
            Move_To_Waypoint();
        }
    }

    void Move_To_Waypoint()
    {
        if (waypoints.Length == 0 || currentHealth <= 0)
        {
            Debug.LogError("웨이포인트가 설정되지 않았거나 체력이 0 이하입니다!");
            return;
        }

        // 현재 위치에서 목표 지점까지 이동
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, moveSpeed * Time.deltaTime);

        // 만약 적이 목표 지점에 도착하면 다음 지점으로 이동
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;


            if (currentWaypointIndex == 0)
            {
                animator.SetBool("Right", false);
                animator.SetBool("Left", false);
                animator.SetBool("Back", true);
            }
            else if (currentWaypointIndex == 1)
            {
                animator.SetBool("Right", false);
                animator.SetBool("Left", true);
                animator.SetBool("Back", false);
            }
            else if (currentWaypointIndex == 2)
            {
                animator.SetBool("Right", false);
                animator.SetBool("Left", true);
                animator.SetBool("Back", false);
            }
            else if (currentWaypointIndex == 3)
            {
                animator.SetBool("Right", true);
                animator.SetBool("Left", false);
                animator.SetBool("Back", false);
            }

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Take_Damage(10);
        }
        else if (other.CompareTag("Player"))
        {
            Player_HpBar.curHp -= 10;
            Debug.Log(Player_HpBar.curHp);

        }
    }

    void Take_Damage(int amount)
    {
        //Debug.Log("damage");
        // Ghost의 체력을 감소시키고, 체력이 0 이하로 떨어지면 Ghost를 제거
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            animator.SetBool("die", true);
            deathSound.Play();
        }

        healthBar.Set_Health(currentHealth);
    }

    void UpdateAnimation()
    {
        // 애니메이션을 설정하는 로직 추가
        if (animator != null)
        {
            // currentWaypointIndex에 따라서 애니메이션 파라미터 설정
            animator.SetInteger("WaypointIndex", currentWaypointIndex);
        }
    }

    public void Destroy_Ghost()
    {
        Destroy(gameObject);
    }
}