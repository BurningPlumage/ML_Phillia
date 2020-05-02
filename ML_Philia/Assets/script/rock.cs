using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rock : MonoBehaviour
{
    private float time_count = 0.7f;

    private void Update()
    {
        time_count -= Time.deltaTime;

        if (time_count <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.name == "agu")
        {
            AI.robot.is_rock = true;
        }
    }

}
