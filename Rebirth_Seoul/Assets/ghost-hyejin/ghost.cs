using UnityEngine;
using UnityEngine.UI;

public class ghost : MonoBehaviour
{
    public Transform[] waypoints; // empty 배열의 위치를 저장할 배열
    public float moveSpeed = 5f; // 이동 속도
    public int maxHealth = 100;   // 최대 체력
    private int currentHealth;     // 현재 체력

    private int currentWaypointIndex = 0;
    private ghostHealthBar healthBar;
    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth; // 최대 체력으로 시작
        healthBar = GetComponentInChildren<ghostHealthBar>();

        animator = GetComponent<Animator>();

        if (healthBar != null)
        {
            healthBar.Set_MaxHealth(maxHealth);
        }
    }

    void Update()
    {
        Move_To_Waypoint();
        UpdateAnimation();
    }

    void Move_To_Waypoint()
    {
        if (waypoints.Length == 0)
        {
            Debug.LogError("웨이포인트가 설정되지 않았습니다!");
            return;
        }

        // 현재 위치에서 목표 지점까지 이동
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, moveSpeed * Time.deltaTime);

        // 만약 적이 목표 지점에 도착하면 다음 지점으로 이동
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Bullet"))
        {
            Take_Damage(10);

            // 플레이어의 체력을 감소시키고 적을 제거
            /*PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
                TakeDamage(damageAmount);
            }*/
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
            //Destroy(gameObject);
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

    public void Destriy_Ghost()
    {
        Destroy(gameObject);
    }
}