using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float towerTime;
    public Vector2 direction;
    public LineRenderer lineRenderer;
    private float spawnPoint;

    // Start is called before the first frame update
    void Awake()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;
        InvokeRepeating("Play", towerTime, towerTime);

        //InvokeRepeating("Razer", towerTime, towerTime);
        //spawnPoint
    }

    public void Play(Vector3 from, Vector3 to)
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, from);
        lineRenderer.SetPosition(1, to);

        Invoke("Stop", towerTime);
    }

    public void Stop()
    {
        lineRenderer.enabled = false;
    }

    void Razer()
    {
        //lineRenderer.gameObject.SetActive(true);
    }
}
