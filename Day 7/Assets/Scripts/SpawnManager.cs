using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    private Vector3 spawnPos = new Vector3(35, 1.4f, 0);
    private float timeDelay = 2;
    private float rateDelay = 4;

    private PlayerController playerControllerScripts;
    // Start is called before the first frame update
    void Start()
    {
        float obstacleIndex = Random.Range(1, 3);
        InvokeRepeating("SpawnObstacle", timeDelay, rateDelay);
        playerControllerScripts = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    public void SpawnObstacle()
    {
        int obstacleIndex = Random.Range(1, 5);
        if (playerControllerScripts.isGameOver == false)
        {
            Instantiate(obstaclePrefabs[obstacleIndex], spawnPos, obstaclePrefabs[obstacleIndex].transform.rotation);
        }


    }
}
