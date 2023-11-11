using UnityEngine;
using UnityEngine.UI;

public class ghost : MonoBehaviour
{
    public Transform[] waypoints; // empty �迭�� ��ġ�� ������ �迭
    public float moveSpeed = 5f; // �̵� �ӵ�
    public int maxHealth = 100;   // �ִ� ü��
    private int currentHealth;     // ���� ü��

    private int currentWaypointIndex = 0;
    private ghostHealthBar healthBar;
    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth; // �ִ� ü������ ����
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
            Debug.LogError("��������Ʈ�� �������� �ʾҽ��ϴ�!");
            return;
        }

        // ���� ��ġ���� ��ǥ �������� �̵�
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, moveSpeed * Time.deltaTime);

        // ���� ���� ��ǥ ������ �����ϸ� ���� �������� �̵�
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

            // �÷��̾��� ü���� ���ҽ�Ű�� ���� ����
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
        // Ghost�� ü���� ���ҽ�Ű��, ü���� 0 ���Ϸ� �������� Ghost�� ����
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
        // �ִϸ��̼��� �����ϴ� ���� �߰�
        if (animator != null)
        {
            // currentWaypointIndex�� ���� �ִϸ��̼� �Ķ���� ����
            animator.SetInteger("WaypointIndex", currentWaypointIndex);
        }
    }

    public void Destriy_Ghost()
    {
        Destroy(gameObject);
    }
}