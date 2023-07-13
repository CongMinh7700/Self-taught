using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloWord : MonoBehaviour
{
    public string myMessage = "Minh ne";
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            Debug.Log(myMessage);
         
        }
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }
}
