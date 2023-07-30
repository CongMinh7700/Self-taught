using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;
    //Spawn up
    public float spawnRangeX = 16.0f;
    public float spawnPosZ = 25.0f;
    public float delayStart = 2;
    public float spawnInterval = 5;
    private int animalIndex;
    //Left and right
    private float sideSpawnX = 17.0f;
    private float minRangeZ = 2.0f;
    private float maxRangeZ = 20.0f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomAnimals", delayStart, spawnInterval);
        InvokeRepeating("SpawnRandomAnimalsLeft", delayStart, spawnInterval);
        InvokeRepeating("SpawnRandomAnimalsRight", delayStart, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {


    }
    void SpawnRandomAnimals()
    {
        animalIndex = Random.Range(0, animalPrefabs.Length);
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ);
        Instantiate(animalPrefabs[animalIndex], spawnPos, animalPrefabs[animalIndex].transform.rotation);
    }
    void SpawnRandomAnimalsLeft()
    {
         animalIndex = Random.Range(0, animalPrefabs.Length);
        Vector3 spawnPos = new Vector3(-sideSpawnX, 0, Random.Range(minRangeZ, maxRangeZ));
        Vector3 rotation = new Vector3(0, 90, 0);
        Instantiate(animalPrefabs[animalIndex], spawnPos, Quaternion.Euler(rotation));
    }  
    void SpawnRandomAnimalsRight()
    {
         animalIndex = Random.Range(0, animalPrefabs.Length);
        Vector3 spawnPos = new Vector3(sideSpawnX, 0, Random.Range(minRangeZ, maxRangeZ));
        Vector3 rotation = new Vector3(0, -90, 0);
        Instantiate(animalPrefabs[animalIndex], spawnPos, Quaternion.Euler(rotation));
    }
}
