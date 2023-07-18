using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    private float Delay = 0;
    private float timeDelay = 0.5f;
    
    // Update is called once per frame
    void Update()
    {
        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time > Delay)
            {
                Delay = Time.time + timeDelay;
                Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);

            }
        }
    }

}
