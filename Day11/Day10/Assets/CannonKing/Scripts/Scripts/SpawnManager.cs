using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] List<GameObject> _enemyPrefabs;
    private float xMax = 75;
    private float xMin = 50;
    private float zRange = 21;
    public void SpawnEnemy()
    {
        int index = Random.Range(0, _enemyPrefabs.Count);
        Vector3 _spawnPos = new Vector3(Random.Range(-xMax, -xMin),0,Random.Range(-zRange,zRange));
        Instantiate(_enemyPrefabs[index], _spawnPos, _enemyPrefabs[index].transform.rotation);
        
    }
}
