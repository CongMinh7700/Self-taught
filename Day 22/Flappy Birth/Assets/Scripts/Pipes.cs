using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipes : MonoBehaviour
{

    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _leftBound;
    void Start()
    {
        _leftBound = Camera.main.ScreenToWorldPoint(Vector3.zero).x;
    }


    void Update()
    {
        MoveLeft();
        DestroyOutOfBound();
    }
    private void MoveLeft()
    {
        transform.position += Vector3.left * Time.deltaTime * _speed;
    }
    private void DestroyOutOfBound()
    {
        if (transform.position.x < _leftBound - 1.5f)
        {
            Destroy(gameObject);
        }
    }
}
