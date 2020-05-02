using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class position_layer : MonoBehaviour
{
    private SpriteRenderer image;

    private void Start()
    {
        image = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        image.sortingOrder = -Mathf.RoundToInt(transform.position.z * 100);
    }
}
