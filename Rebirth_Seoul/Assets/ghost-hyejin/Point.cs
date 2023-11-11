using UnityEngine;

public class Point : MonoBehaviour
{
    public GameObject prefabToSpawn; // ������ ������
    public Transform spawnPoint; // ���� ��ġ
    public int pointHP;

    void Start()
    {
        // 30�ʸ��� SpawnPrefab �Լ��� ȣ��
        InvokeRepeating("SpawnPrefab", 0f, 30f);
        pointHP = 100;
    }

    void Update()
    {
        if (pointHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    void SpawnPrefab()
    {
        // ���� ��ġ���� �������� ����
        GameObject ghostInstance = Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);

        // ������ ghost �ν��Ͻ��� ghost ��ũ��Ʈ�� waypoints ����
        ghost ghostScript = ghostInstance.GetComponent<ghost>();
        if (ghostScript != null)
        {
           /* Transform waypoint0.position = transform.GetChild(0).position;
            Transform waypoint1.position = transform.GetChild(1).position;





            ghostScript.waypoints = new Transform[2]; // ������ �迭 ũ��� ����
            ghostScript.waypoints[0] = transform.GetChild(0); // ���÷� ù ��° �ڽ� ������Ʈ�� �Ҵ�
            ghostScript.waypoints[1] = transform.GetChild(1); // ���÷� �� ��° �ڽ� ������Ʈ�� �Ҵ�
           */
        }
        else
        {
            Debug.LogError("Ghost ��ũ��Ʈ�� ã�� �� �����ϴ�.");
        }
    }
}