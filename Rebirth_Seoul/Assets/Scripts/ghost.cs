//using System.Diagnostics;
using UnityEngine;

public class ghost : MonoBehaviour
{
    public Transform[] waypoints; // empty �迭�� ��ġ�� ������ �迭
    public float moveSpeed = 5f; // �̵� �ӵ�
    public int damageAmount = 10; // �÷��̾ ���� ������ ��

    private int currentWaypointIndex = 0;

    void Update()
    {
        MoveToWaypoint();
    }

    void MoveToWaypoint()
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

    void OnTriggerEnter(Collider other)
    {
        // �÷��̾�� �ε����� ��
        if (other.CompareTag("Player"))
        {
            // �÷��̾��� ü���� ���ҽ�Ű�� ���� ����
            /*PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
                Destroy(gameObject);
            }*/
        }
    }
}