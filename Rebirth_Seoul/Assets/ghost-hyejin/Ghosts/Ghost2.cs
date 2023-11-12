using UnityEngine;
using UnityEngine.UI;

public class Ghost2 : Ghost_Parent
{
    protected override void Move_To_Waypoint()
    {
        if (waypoints.Length == 0 || currentHealth <= 0)
        {
            Debug.LogError("웨이포인트가 설정되지 않았거나 체력이 0 이하입니다!");
            return;
        }

        // 현재 위치에서 목표 지점까지 이동
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, moveSpeed * Time.deltaTime);

        // 만약 적이 목표 지점에 도착하면 다음 지점으로 이동
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