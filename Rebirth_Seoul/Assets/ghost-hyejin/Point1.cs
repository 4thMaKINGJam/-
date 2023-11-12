using UnityEngine;

public class Point1 : MonoBehaviour
{
    public GameObject prefabToSpawn; // 생성할 프리팹
    public Transform spawnPoint; // 생성 위치
    public int pointHP;
    public int damage;

    void Start()
    {
        // 30초마다 SpawnPrefab 함수를 호출
        InvokeRepeating("SpawnPrefab", 0f, 30f);
        pointHP = 100;
        damage = 10;
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
        // 충돌한 오브젝트의 태그가 "Bullet"인지 확인
        if (other.CompareTag("Bullet"))
        {
            // 포인트의 HP 감소
            pointHP -= damage;
        }
    }

    void SpawnPrefab()
    {
        // 현재 위치에서 프리팹을 생성
        GameObject ghostInstance = Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);

        // 생성된 ghost 인스턴스의 ghost 스크립트에 waypoints 설정
        Ghost1 ghostScript = ghostInstance.GetComponent<Ghost1>();
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
            Debug.LogError("Ghost 스크립트를 찾을 수 없습니다.");
        }
    }
}