using UnityEngine;

public class Point : MonoBehaviour
{
    public GameObject prefabToSpawn; // 생성할 프리팹
    public Transform spawnPoint; // 생성 위치
    public int pointHP;

    void Start()
    {
        // 30초마다 SpawnPrefab 함수를 호출
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
        // 현재 위치에서 프리팹을 생성
        GameObject ghostInstance = Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);

        // 생성된 ghost 인스턴스의 ghost 스크립트에 waypoints 설정
        ghost ghostScript = ghostInstance.GetComponent<ghost>();
        if (ghostScript != null)
        {
           /* Transform waypoint0.position = transform.GetChild(0).position;
            Transform waypoint1.position = transform.GetChild(1).position;





            ghostScript.waypoints = new Transform[2]; // 적절한 배열 크기로 설정
            ghostScript.waypoints[0] = transform.GetChild(0); // 예시로 첫 번째 자식 오브젝트를 할당
            ghostScript.waypoints[1] = transform.GetChild(1); // 예시로 두 번째 자식 오브젝트를 할당
           */
        }
        else
        {
            Debug.LogError("Ghost 스크립트를 찾을 수 없습니다.");
        }
    }
}