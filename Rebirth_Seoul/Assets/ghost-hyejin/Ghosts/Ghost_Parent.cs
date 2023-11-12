using UnityEngine;
using UnityEngine.UI;

public class Ghost_Parent : MonoBehaviour
{
    public Transform[] waypoints; // empty �迭�� ��ġ�� ������ �迭
    public float moveSpeed = 5f; // �̵� �ӵ�
    public int maxHealth = 100;   // �ִ� ü��
    public int currentHealth;     // ���� ü��

    protected GameManager worldGM;
    public GameObject GM;

    public int currentWaypointIndex = 0;
    protected ghostHealthBar healthBar;
    protected Animator animator;

    public AudioSource deathSound;

    protected void Start()
    {
        currentHealth = maxHealth; // �ִ� ü������ ����
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