using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float spineTime; // spineTime마다 반복
    public float spineStay; //spineTime동안 유지
    public GameObject GM;
    private GameManager worldGM;

    void Start()
    {
        //초기화
        gameObject.SetActive(false);
        worldGM = GM.GetComponent<GameManager>();

        //지연 시간
        InvokeRepeating("spine_active", spineTime, spineTime);
    }

    private void spine_active()
    {
        Debug.Log("Spine activated");
        gameObject.SetActive(true);
        //isActive = true;

        //Stay 후에 inactive
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
