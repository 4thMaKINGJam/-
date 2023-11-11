using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float towerTime; //towerTime에 한 번씩 빔 발사
    public float beemTime; // beemTime 길이만큼 빔 발사
    public float startAngle; //시작 각도
    public float moveAngle; //움직일 각도
    public float dir; //디렉션. 반시계방향:1.0 시계방향:-1.0
    public float length; // 빔 길이
    public string target;

    private float offset; // 한 번에 증가할 각도
    private float nowAngle; //현재 각도
    private float time; //현재 시간
    private Vector3 from; // 빔 시작 위치
    private Vector3 to; //빔 끝나는 위치

    public LineRenderer lineRenderer;

    void Awake()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;
        from = this.gameObject.transform.position; //빔 시작 좌표 = 타워 위치
        InvokeRepeating("razer_start", 0, towerTime); //towerTime에 한 번씩 레이저 스타트
    }

    public void razer_start()
    {
        Debug.Log("beem");
        time = 0.0f; // 시간 초기화
        nowAngle = 0.0f; // 시작 앵글 초기화
        to = from + ConvertAngleToVector(startAngle)*length; // 첫 빔 발사 위치
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, from);
        lineRenderer.SetPosition(1, to);

        StartCoroutine("razer_rotate");
        Invoke("razer_stop", beemTime + 0.1f);
    }

    public void razer_stop()
    {
        Debug.Log("beem Stop");
        lineRenderer.enabled = false;
    }

    private Vector3 ConvertAngleToVector(float _deg) // 각도를 벡터로 변환하는 함수
    {
        var rad = _deg * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(rad), Mathf.Sin(rad));
    }

    IEnumerator razer_rotate()
    {
        while(time < beemTime)
        {
            yield return new WaitForSeconds(0.1f);

            nowAngle += moveAngle/(10.0f*beemTime);
            to = ConvertAngleToVector(startAngle + nowAngle) * length;
            lineRenderer.SetPosition(1, from + to);
            RaycastHit2D hit = Physics2D.Raycast(from, to, length, LayerMask.GetMask("Player"));
            Debug.DrawRay(from, to, Color.red, 2.0f,false);
            if(hit.collider != null)
            {
                Debug.Log(hit.collider.name);
            }
            time += 0.1f;
        }
    }
}
