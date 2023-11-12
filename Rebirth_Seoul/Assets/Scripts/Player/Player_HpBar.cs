using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_HpBar : MonoBehaviour
{
    [SerializeField]
    private Slider hpbar;
    private float maxHp = 100;

    public static float curHp = 100;
    public bool HpZero { get; private set; } = false;
    private GameManager worldGM;
    public GameObject GM;


    void Start()
    {
        worldGM = GM.GetComponent<GameManager>();
        hpbar.value = (float)worldGM.PlayerHP / (float)maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            worldGM.GetDamage(10);
        }

        HandleHp();

        if (worldGM.PlayerHP <= 0)
        {
            HpZero = true;
            Invoke("LoadGameOverScene", 1.5f);
        }
    }

    private void HandleHp()
    {
        hpbar.value = (float)worldGM.PlayerHP / (float)maxHp;
    }

    void LoadGameOverScene()
    {
        SceneManager.LoadScene("End_GameOver");
    }
}
