using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBound : MonoBehaviour
{
    public float xRange = 50.0f;

    void Update()
    {
        if(transform.position.x < -xRange)
        {
            Destroy(gameObject);
        }
    }
}
