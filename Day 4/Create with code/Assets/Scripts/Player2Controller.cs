using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    private float _speed = 10.0f;
    private float _turnSpeed = 150.0f;
    private float _horizontalInput;
    private float _forwardInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() { 
         _horizontalInput = Input.GetAxis("Horizontal2");
        _forwardInput = Input.GetAxis("Vertical"); 
        //Move Straight
        transform.Translate(Vector3.forward* Time.deltaTime* _speed * _forwardInput);
        //Return tank
        transform.Rotate(Vector3.up, Time.deltaTime* _turnSpeed* _horizontalInput);


}
}
