using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public GameObject _player;
    private Vector3 Offset = new Vector3(0, 7, -9);
    private Vector3 Offset1 = new Vector3(0, 5, -1);
    private bool isScriptsEnable = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.CapsLock))
        {
            isScriptsEnable = !isScriptsEnable;
        }
        if (isScriptsEnable)
        {

       
        //vi tri cpua nguoi choi +vi tri cua camera
        transform.position = _player.transform.position + Offset;
        }
        else
        {
            transform.position = _player.transform.position + Offset1;
        }
    }
}
