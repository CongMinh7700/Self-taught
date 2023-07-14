using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 20;
    private PlayerController playerControllerScript;
    private float leftBound = -5;
  
    // Start is called before the first frame update
    
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        MoveObject();
        DestroyOutOfBound();    
       
    }
    public void MoveObject()
    {
        if (playerControllerScript.isGameOver == false && playerControllerScript.isSpeedRun == false)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
         if(playerControllerScript.isGameOver == false && playerControllerScript.isSpeedRun == true)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed * 3);
        }
       
    }
    public void DestroyOutOfBound()
    {
        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            //Debug.Log("Destroy Obstacle");
            Destroy(gameObject);
        }
    }
}
