using UnityEngine;
using UnityEngine.UI;

public class Point2 : MonoBehaviour
{
    public GameObject prefabToSpawn; // 생성할 프리팹
    public Transform spawnPoint; // 생성 위치
    public int pointHP;
    public int damage;

    public Slider healthSlider; // 체력 바
    public int maxPointHP = 100;

    void Start()
    {
        // 30초마다 SpawnPrefab 함수를 호출
        InvokeRepeating("SpawnPrefab", 0f, 30f);
        pointHP = maxPointHP; // 최대 체력으로 초기화
        damage = 10;

        // 체력 바 초기화
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
        // 충돌한 오브젝트의 태그가 "Bullet"인지 확인
        if (other.CompareTag("Bullet"))
        {
            // 포인트의 HP 감소
            pointHP -= damage;

            // 체력 바 업데이트
            UpdateHealthBar();
        }
    }

    void SpawnPrefab()
    {
        // 현재 위치에서 프리팹을 생성
        GameObject ghostInstance = Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);

        // 생성된 ghost 인스턴스의 ghost 스크립트에 waypoints 설정
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
            Debug.LogError("Ghost 스크립트를 찾을 수 없습니다.");
        }
    }

    void UpdateHealthBar()
    {
        // 체력 바 업데이트
        if (healthSlider != null)
        {
            healthSlider.value = pointHP;
        }
    }
}