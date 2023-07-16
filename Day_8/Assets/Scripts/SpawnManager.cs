using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public int bossRound;
    public GameObject bossPrefabs;
    public GameObject[] miniEnemyPrefabs;
    public GameObject[] enemyPrefabs;
    
    private int randomIndex;
    public GameObject[] powerupPrefabs;
    public float spawnRange = 9;
    public int enemyCount ;
    public int waveNumber = 1;

    // Start is called before the first frame update
    void Start()
    {
        int randomPowerUp = Random.Range(0, powerupPrefabs.Length);
        SpawnEnemyWave(waveNumber);
        Instantiate(powerupPrefabs[randomPowerUp], GenerateSpawnPosition(), powerupPrefabs[randomPowerUp].transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
       
        enemyCount = FindObjectsOfType<EnemyController>().Length;
        if(enemyCount == 0)
        {
            waveNumber++;
            if(waveNumber %bossRound == 0)
            {
                SpawnBossWave(waveNumber);
            }
            else
            {
                SpawnEnemyWave(waveNumber);
            }
          
            int randomPowerUp = Random.Range(0, powerupPrefabs.Length);
            Instantiate(powerupPrefabs[randomPowerUp], GenerateSpawnPosition(), powerupPrefabs[randomPowerUp].transform.rotation);
        }
    }
    public void SpawnEnemyWave(int enemiesToSpawn)
    {
        randomIndex = Random.Range(0, enemyPrefabs.Length);
        for(int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefabs[randomIndex], GenerateSpawnPosition(), enemyPrefabs[randomIndex].transform.rotation);
           
        }
    }
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.RandomRange(-spawnRange, spawnRange);
        float spawnPosZ = Random.RandomRange(-spawnRange, spawnRange);
        Vector3 spawnPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return spawnPos;
    }
    public void SpawnBossWave(int currentRound)
    {
        int miniEnemysToSpawn;
        if(bossRound != 0)
        {
            miniEnemysToSpawn = currentRound / bossRound;
        }
        else
        {
            miniEnemysToSpawn = 1;
        }
        var boss = Instantiate(bossPrefabs, GenerateSpawnPosition(), bossPrefabs.transform.rotation);
        boss.GetComponent<EnemyController>().minienemySpawnCount = miniEnemysToSpawn;

    }
   public void SpawnMiniEnemy(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            int randomMini = Random.Range(0, miniEnemyPrefabs.Length);
            Instantiate(miniEnemyPrefabs[randomMini], GenerateSpawnPosition(), miniEnemyPrefabs[randomMini].transform.rotation);
        }
    }
}
