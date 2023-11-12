using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHp : MonoBehaviour
{
    public int towerHP;
    public int damage;
    Tower tower;

    void Start()
    {
        tower = this.GetComponent<Tower>();
        towerHP = 100;
        damage = 10;
    }

    void Update()
    {
        if (towerHP <= 0)
        {
            tower.team = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // �浹�� ������Ʈ�� �±װ� "Bullet"���� Ȯ��
        if (other.CompareTag("Bullet"))
        {
            // ����Ʈ�� HP ����
            towerHP -= damage;
        }
    }

}
