using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "agu")
        {
            AI.robot.is_fall = true;
        }
    }
}
