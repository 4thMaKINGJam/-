using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float towerTime; //towerTime�� �� ���� �� �߻�
    public float beemTime; // beemTime ���̸�ŭ �� �߻�
    public float startAngle; //���� ����
    public float moveAngle; //������ ����
    public float dir; //�𷺼�. �ݽð����:1.0 �ð����:-1.0
    public float length; // �� ����
    public string target;

    private float offset; // �� ���� ������ ����
    private float nowAngle; //���� ����
    private float time; //���� �ð�
    private Vector3 from; // �� ���� ��ġ
    private Vector3 to; //�� ������ ��ġ

    public LineRenderer lineRenderer;

    void Awake()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;
        from = this.gameObject.transform.position; //�� ���� ��ǥ = Ÿ�� ��ġ
        InvokeRepeating("razer_start", 0, towerTime); //towerTime�� �� ���� ������ ��ŸƮ
    }

    public void razer_start()
    {
        Debug.Log("beem");
        time = 0.0f; // �ð� �ʱ�ȭ
        nowAngle = 0.0f; // ���� �ޱ� �ʱ�ȭ
        to = from + ConvertAngleToVector(startAngle)*length; // ù �� �߻� ��ġ
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

    private Vector3 ConvertAngleToVector(float _deg) // ������ ���ͷ� ��ȯ�ϴ� �Լ�
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
