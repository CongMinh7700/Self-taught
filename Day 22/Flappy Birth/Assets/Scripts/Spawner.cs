using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefabs;
    [SerializeField] private float _maxheight = 1.0f;
    [SerializeField] private float _minheight = -1.0f;
    [SerializeField] private float spawnRate = 1.0f;

    private void OnEnable()
    {
        InvokeRepeating("Spawn", spawnRate, spawnRate);
    }
    private void OnDisable()
    {
        CancelInvoke("Spawn");
    }

    private void Spawn()
    {
        GameObject pipes = Instantiate(_prefabs, transform.position, Quaternion.identity);
        pipes.transform.position += Vector3.up * Random.Range(_minheight, _maxheight);
    }
}
