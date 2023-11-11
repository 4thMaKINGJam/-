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
            Transform parentTransform = transform.parent;

            ghostScript.waypoints = new Transform[2];
            ghostScript.waypoints[0] = parentTransform.GetChild(1);
            ghostScript.waypoints[1] = parentTransform.GetChild(2);
        }
        else
        {
            Debug.LogError("Ghost ��ũ��Ʈ�� ã�� �� �����ϴ�.");
        }
    }
}