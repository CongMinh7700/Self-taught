using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    private GameObject player;
    private Rigidbody enemyRB;
    public bool isBoss = false;
    public float spawnInterval;
    private float nextSpawn;
    public int minienemySpawnCount;
    private SpawnManager spawn;
    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        if (isBoss)
        {
            spawn = FindObjectOfType<SpawnManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRB.AddForce( lookDirection * speed);
        if (isBoss)
        {
            if(Time.time > nextSpawn)
            {
                nextSpawn = Time.time + spawnInterval;
                spawn.SpawnMiniEnemy(minienemySpawnCount);
            }
        }
        if(transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
