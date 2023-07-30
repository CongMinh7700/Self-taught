using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRB;
    private GameManager gameManager;
    public ParticleSystem particleExploision;
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -2;
    public int pointValue;
    
    // Start is called before the first frame update
    void Start()
    {
        targetRB = GetComponent<Rigidbody>();
        targetRB.AddForce(RandomForce(), ForceMode.Impulse);
        targetRB.AddTorque(RandomTorque(), RandomTorque(), RandomTorque());
        transform.position = RandomSpawnPos();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }
    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }
    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
    private void OnMouseDown()
    {
        if (gameManager.isGameActive) { 
        Destroy(gameObject);
        Instantiate(particleExploision, transform.position, particleExploision.transform.rotation);
        gameManager.UpdateScore(pointValue);
       
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad") )
        {
            if(gameManager.isLive == true)
            {
                gameManager.UpdateLives(-1);
            }
            
           
        }
    }
    public void DestroyTarget()
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(particleExploision, transform.position, particleExploision.transform.rotation);
            gameManager.UpdateScore(pointValue);
        }
    }
}

