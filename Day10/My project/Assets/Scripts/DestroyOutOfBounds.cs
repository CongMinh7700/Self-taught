using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float topBounds = 30.0f;
    private float downBounds = -10.0f;
    private float horizontalBounds = 25.0f;
    private GameManager gameManager;
 

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z > topBounds )
        {
            Destroy(gameObject);
        }
        else if( transform.position.z < downBounds)
        {
            gameManager.AddLives(-1);
            Destroy(gameObject);
        }
        if(transform.position.x > horizontalBounds)
        {
            gameManager.AddLives(-1);
            Destroy(gameObject);
        }else if (transform.position.x < -horizontalBounds)
        {
            gameManager.AddLives(-1);
            Destroy(gameObject);
        }
        
    }
}
