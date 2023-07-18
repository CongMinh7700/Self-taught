using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float _horizontalInput;
    public float _verticalInput;
    private float _speed = 20.0f;
    private float _limited = 15.0f;
    private float _limitedTopZ = 5.0f;
    private float _limitedDownZ = 0.0f;
    public GameObject _foodPrefabs;
    public Transform _projectitleSpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
        
    {
        if (transform.position.x < -_limited)
        {
            transform.position = new Vector3(-_limited, transform.position.y, transform.position.z);
        } else if (transform.position.x > _limited)
        {
            transform.position = new Vector3(_limited, transform.position.y, transform.position.z);
        }
        if (transform.position.z < _limitedDownZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, _limitedDownZ);
        }
        else if (transform.position.z > _limitedTopZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, _limitedTopZ);
        }

        _horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * _horizontalInput * Time.deltaTime * _speed);
            _verticalInput = Input.GetAxis("Vertical");
            transform.Translate(Vector3.forward * _verticalInput * Time.deltaTime * _speed);
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Shoot a food 
            Instantiate(_foodPrefabs, _projectitleSpawnPoint.position, _foodPrefabs.transform.rotation);
        }
        
    }
}
