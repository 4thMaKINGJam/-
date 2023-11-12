using UnityEngine;
using UnityEngine.UI;

public class Point2 : MonoBehaviour
{
    public GameObject prefabToSpawn; // ������ ������
    public Transform spawnPoint; // ���� ��ġ
    public int pointHP;
    public int damage;

    public Slider healthSlider; // ü�� ��
    public int maxPointHP = 100;

    void Start()
    {
        // 30�ʸ��� SpawnPrefab �Լ��� ȣ��
        InvokeRepeating("SpawnPrefab", 0f, 30f);
        pointHP = maxPointHP; // �ִ� ü������ �ʱ�ȭ
        damage = 10;

        // ü�� �� �ʱ�ȭ
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxPointHP;
            healthSlider.value = pointHP;
        }
    }

    void Update()
    {
        if (pointHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // �浹�� ������Ʈ�� �±װ� "Bullet"���� Ȯ��
        if (other.CompareTag("Bullet"))
        {
            // ����Ʈ�� HP ����
            pointHP -= damage;

            // ü�� �� ������Ʈ
            UpdateHealthBar();
        }
    }

    void SpawnPrefab()
    {
        // ���� ��ġ���� �������� ����
        GameObject ghostInstance = Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);

        // ������ ghost �ν��Ͻ��� ghost ��ũ��Ʈ�� waypoints ����
        Ghost2 ghostScript = ghostInstance.GetComponent<Ghost2>();
        if (ghostScript != null)
        {
            Transform parentTransform = transform.parent;

            ghostScript.waypoints = new Transform[4];
            ghostScript.waypoints[0] = parentTransform.GetChild(1);
            ghostScript.waypoints[1] = parentTransform.GetChild(2);
            ghostScript.waypoints[2] = parentTransform.GetChild(3);
            ghostScript.waypoints[3] = parentTransform.GetChild(4);
        }
        else
        {
            Debug.LogError("Ghost ��ũ��Ʈ�� ã�� �� �����ϴ�.");
        }
    }

    void UpdateHealthBar()
    {
        // ü�� �� ������Ʈ
        if (healthSlider != null)
        {
            healthSlider.value = pointHP;
        }
    }
}