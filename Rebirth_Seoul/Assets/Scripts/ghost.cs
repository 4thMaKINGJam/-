//using System.Diagnostics;
using UnityEngine;

public class ghost : MonoBehaviour
{
    public Transform[] waypoints; // empty 배열의 위치를 저장할 배열
    public float moveSpeed = 5f; // 이동 속도
    public int damageAmount = 10; // 플레이어에 대한 데미지 양

    private int currentWaypointIndex = 0;

    void Update()
    {
        MoveToWaypoint();
    }

    void MoveToWaypoint()
    {
        if (waypoints.Length == 0)
        {
            Debug.LogError("웨이포인트가 설정되지 않았습니다!");
            return;
        }

        // 현재 위치에서 목표 지점까지 이동
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, moveSpeed * Time.deltaTime);

        // 만약 적이 목표 지점에 도착하면 다음 지점으로 이동
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // 플레이어와 부딪혔을 때
        if (other.CompareTag("Player"))
        {
            // 플레이어의 체력을 감소시키고 적을 제거
            /*PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
                Destroy(gameObject);
            }*/
        }
    }
}