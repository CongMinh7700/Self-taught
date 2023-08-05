using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRow : MonoBehaviour
{
    public TileCell[] cells { get; private set; }

    private void Awake()
    { 
        //tìm các thành phần TileCell trả về dạng list
        cells = GetComponentsInChildren<TileCell>();
    }
}
