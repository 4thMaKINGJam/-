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
    public Slider healthSlider; // 체력 바
    private int maxTowerHP = 100;
    SpriteRenderer Tsprite;

    void Start()
    {
        tower = this.GetComponent<Tower>();
        towerHP = maxTowerHP;
        damage = 10;
        Tsprite = gameObject.GetComponent<SpriteRenderer>();

        // 체력 바 초기화
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
        // 충돌한 오브젝트의 태그가 "Bullet"인지 확인
        if (other.CompareTag("Bullet"))
        {
            // 포인트의 HP 감소
            towerHP -= damage;

            // 체력 바 업데이트
            UpdateHealthBar();
        }
    }

    void UpdateHealthBar()
    {
        // 체력 바 업데이트
        if (healthSlider != null)
        {
            healthSlider.value = towerHP;
        }
    }
}
