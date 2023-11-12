using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public float PlayerHP;


    // Start is called before the first frame update
    void Start()
    {
        PlayerHP = 100;
    }

    public void GetDamage(int damage)
    {
        PlayerHP -= damage;
    }
}
