using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndingHP : MonoBehaviour
{
    public TextMeshProUGUI Score_text;

    // Start is called before the first frame update
    void Start()
    {
        Score_text.text = "남은 체력: " + PlayerData.PlayerHP;
    }
}
