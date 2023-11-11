using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float spineTime; // spineTime���� �ݺ�
    public float spineStay; //spineTime���� ����
    //bool isActive = false;

    void Start()
    {
        //�ʱ�ȭ
        gameObject.SetActive(false);

        //���� �ð�
        InvokeRepeating("spine_active", spineTime, spineTime);
    }

    private void spine_active()
    {
        Debug.Log("Spine activated");
        gameObject.SetActive(true);
        //isActive = true;

        //Stay �Ŀ� inactive
        Invoke("spine_inactive", spineStay);
    }

    private void spine_inactive()
    {
        //isActive = false;
        gameObject.SetActive(false);
        Debug.Log("Spine Inactivated");
    }

    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.tag == "Player")
        {
            Debug.Log("Spine Damage");
        }
    }
}
