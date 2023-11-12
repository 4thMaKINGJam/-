using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManualAppear : MonoBehaviour
{
    public RawImage Manual;
    // Start is called before the first frame update
    void Start()
    {
        Manual.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.M))
        {
            Manual.enabled = true;
        }
        else
        {
            Manual.enabled = false;
        }
    }
}
