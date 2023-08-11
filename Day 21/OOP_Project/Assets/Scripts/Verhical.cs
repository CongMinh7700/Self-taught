using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Verhical : MonoBehaviour
{
    private float _speed;

    //ENCAPSULATION
    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }
    private float _turnSpeed ;
    public float TurnSpeed
    {
        get { return _turnSpeed; }
        set { _turnSpeed = value; }
    }
    //ABTRACT
    public abstract bool isPlayer { get ; }
    //POLYMORPHISM
    public virtual void Move() { }
    
}
