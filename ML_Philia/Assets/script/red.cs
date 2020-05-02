using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class red : MonoBehaviour
{
    private float time;
    private void Start()
    {
        time = 4;
    }
    private void Update()
    {
        if (AI.robot.is_fall)
        {
            Destroy(gameObject);
        }

        time -= Time.deltaTime;
        if (time <= 0) Destroy(gameObject);
    }
}
