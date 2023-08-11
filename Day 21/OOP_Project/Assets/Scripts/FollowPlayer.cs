using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    [SerializeField]private GameObject _player;
    [SerializeField] private Vector3 _offset = new Vector3(0, 7, -9);
    private Vector3 _offset1 = new Vector3(0, 5, -1);
    private bool _isScriptsEnable = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.CapsLock))
        {
            _isScriptsEnable = !_isScriptsEnable;
        }
        if (_isScriptsEnable)
        {

       
        //vi tri cpua nguoi choi +vi tri cua camera
        transform.position = _player.transform.position + _offset;
        }
        else
        {
            transform.position = _player.transform.position + _offset1;
        }
    }
}
