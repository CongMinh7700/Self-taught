using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float speed = 50;
    private Transform target;
    private bool homing;
    private float bulletStrength = 15.0f;
    private float aliveTimer = 5.0f;
    
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {       
        if(homing && target != null)
        {
            Vector3 moveDirection = (target.transform.position - transform.position).normalized;
            transform.position += moveDirection * speed * Time.deltaTime;
            transform.LookAt(target);
        }
    }
    public void Fire(Transform newTarget)
    {
        target = newTarget;
        homing = true;
        Destroy(gameObject, aliveTimer);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(target != null)
        {
            if (collision.gameObject.CompareTag(target.tag))
            {
                Rigidbody targetRigid = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 away = -collision.contacts[0].normal;
                targetRigid.AddForce(away * bulletStrength, ForceMode.Impulse);
                Destroy(gameObject);
            }
        }
    }

}
