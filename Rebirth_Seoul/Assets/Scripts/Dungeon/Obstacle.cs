using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float spineTime; // spineTime���� �ݺ�
    public float spineStay; //spineTime���� ����
    public GameObject GM;
    private GameManager worldGM;

    void Start()
    {
        //�ʱ�ȭ
        gameObject.SetActive(false);
        worldGM = GM.GetComponent<GameManager>();

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
        gameObject.SetActive(false);
        Debug.Log("Spine Inactivated");
    }

    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.tag == "Player")
        {
            worldGM.GetDamage(20);
            Debug.Log("Spine Damage");
        }
    }
}
