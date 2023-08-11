using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
public class BotController : Verhical
{
    private float _speedBot = 5.0f;
    public override bool isPlayer => false;
    // Start is called before the first frame update
    void Start()
    {
        //INHERITANCE
        Speed = _speedBot;
    }
   //POLYMORPHISM
    public override void Move()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * Speed);
    }
}
