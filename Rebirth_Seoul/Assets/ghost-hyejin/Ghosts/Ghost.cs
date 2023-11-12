using UnityEngine;
using UnityEngine.UI;

public class Ghost : MonoBehaviour
{
    public Transform[] waypoints; // empty �迭�� ��ġ�� ������ �迭
    public float moveSpeed = 5f; // �̵� �ӵ�
    public int maxHealth = 100;   // �ִ� ü��
    public int currentHealth;     // ���� ü��

    public int currentWaypointIndex = 0;
    private ghostHealthBar healthBar;
    private Animator animator;

    public AudioSource deathSound;

    void Start()
    {
        currentHealth = maxHealth; // �ִ� ü������ ����
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
            Debug.LogError("��������Ʈ�� �������� �ʾҰų� ü���� 0 �����Դϴ�!");
            return;
        }

        // ���� ��ġ���� ��ǥ �������� �̵�
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, moveSpeed * Time.deltaTime);

        // ���� ���� ��ǥ ������ �����ϸ� ���� �������� �̵�
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
        // Ghost�� ü���� ���ҽ�Ű��, ü���� 0 ���Ϸ� �������� Ghost�� ����
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
        // �ִϸ��̼��� �����ϴ� ���� �߰�
        if (animator != null)
        {
            // currentWaypointIndex�� ���� �ִϸ��̼� �Ķ���� ����
            animator.SetInteger("WaypointIndex", currentWaypointIndex);
        }
    }

    public void Destroy_Ghost()
    {
        Destroy(gameObject);
    }
}