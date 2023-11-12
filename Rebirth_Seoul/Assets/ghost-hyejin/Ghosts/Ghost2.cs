using UnityEngine;
using UnityEngine.UI;

public class Ghost2 : Ghost_Parent
{
    protected override void Move_To_Waypoint()
    {
        if (waypoints.Length == 0 || currentHealth <= 0)
        {
            Debug.LogError("��������Ʈ�� �������� �ʾҰų� ü���� 0 �����Դϴ�!");
            return;
        }

        // ���� ��ġ���� ��ǥ �������� �̵�
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, moveSpeed * Time.deltaTime);

        // ���� ���� ��ǥ ������ �����ϸ� ���� �������� �̵�
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;


            if (currentWaypointIndex == 0)
            {
                animator.SetBool("Right", false);
                animator.SetBool("Left", true);
                animator.SetBool("Back", false);
            }
            else if (currentWaypointIndex == 1)
            {
                animator.SetBool("Right", false);
                animator.SetBool("Left", false);
                animator.SetBool("Back", true);
            }
            else if (currentWaypointIndex == 2)
            {
                animator.SetBool("Right", false);
                animator.SetBool("Left", false);
                animator.SetBool("Back", true);
            }
            else if (currentWaypointIndex == 3)
            {
                animator.SetBool("Right", true);
                animator.SetBool("Left", true);
                animator.SetBool("Back", false);
            }

        }
    }
}