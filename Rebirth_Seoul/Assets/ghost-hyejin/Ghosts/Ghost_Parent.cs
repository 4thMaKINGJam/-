using UnityEngine;
using UnityEngine.UI;

public class Ghost_Parent : MonoBehaviour
{
    public Transform[] waypoints; // empty 배열의 위치를 저장할 배열
    public float moveSpeed = 5f; // 이동 속도
    public int maxHealth = 100;   // 최대 체력
    public int currentHealth;     // 현재 체력

    protected GameManager worldGM;
    public GameObject GM;

    public int currentWaypointIndex = 0;
    protected ghostHealthBar healthBar;
    protected Animator animator;

    public AudioSource deathSound;

    protected void Start()
    {
        currentHealth = maxHealth; // 최대 체력으로 시작
        healthBar = GetComponentInChildren<ghostHealthBar>();

        animator = GetComponent<Animator>();

        GM = GameObject.Find("GameManager");
        worldGM = GM.GetComponent<GameManager>();

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

    protected virtual void Move_To_Waypoint()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Take_Damage(30);
        }
        else if (other.CompareTag("Player"))
        {
            worldGM.GetDamage(30);
            Debug.Log(worldGM.PlayerHP);
        }
    }

    public void Take_Damage(int amount)
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