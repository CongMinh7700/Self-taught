using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : Verhical
{

    private float _speedPlayer = 30.0f;
    private float _turnSpeedPlayer = 150.0f;
    private float _horizontalInput;
    private float _forwardInput;
    public override bool isPlayer => true;
    // Start is called before the first frame update
    void Start()    
    {
        //INHERITANCE
        TurnSpeed = _turnSpeedPlayer;
        Speed = _speedPlayer;
    }

    //POLYMORPHISM
    public override void Move()
    {
        //get input from player
        _horizontalInput = Input.GetAxis("Horizontal");
        _forwardInput = Input.GetAxis("Vertical");
        //Move Straight
        transform.Translate(Vector3.forward * Time.deltaTime * Speed * _forwardInput);
        //Return tank
        transform.Rotate(Vector3.up, Time.deltaTime * TurnSpeed * _horizontalInput);
    }
}

