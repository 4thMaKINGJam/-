using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerHp : MonoBehaviour
{
    public int towerHP;
    public int damage;
    public Sprite tower_team;
    Tower tower;
    public Slider healthSlider; // ü�� ��
    private int maxTowerHP = 100;
    SpriteRenderer Tsprite;

    void Start()
    {
        tower = this.GetComponent<Tower>();
        towerHP = maxTowerHP;
        damage = 10;
        Tsprite = gameObject.GetComponent<SpriteRenderer>();

        // ü�� �� �ʱ�ȭ
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxTowerHP;
            healthSlider.value = towerHP;
        }
    }

    void Update()
    {
        if (towerHP <= 0)
        {
            tower.team = true;
            Tsprite.sprite = tower_team;
            tower.lineRenderer.startColor = Color.cyan;
            tower.lineRenderer.endColor = Color.blue;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // �浹�� ������Ʈ�� �±װ� "Bullet"���� Ȯ��
        if (other.CompareTag("Bullet"))
        {
            // ����Ʈ�� HP ����
            towerHP -= damage;

            // ü�� �� ������Ʈ
            UpdateHealthBar();
        }
    }

    void UpdateHealthBar()
    {
        // ü�� �� ������Ʈ
        if (healthSlider != null)
        {
            healthSlider.value = towerHP;
        }
    }
}
