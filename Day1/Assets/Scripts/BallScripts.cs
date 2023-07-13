using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScripts : MonoBehaviour
{
    public Vector3 rotateChange;
    public Vector3 positionChange;
    public Vector3 scaleChange;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //transform.localScale += scaleChange;
        // transform.localPosition += scaleChange;
        transform.Rotate(rotateChange);
    }
}
