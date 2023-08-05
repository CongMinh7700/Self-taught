using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCell : MonoBehaviour
{
   public Vector2Int coordinates { get; set; }
    public Tile tile { get; set; }
   // public bool empty => tile == null;
   //Kiểm tra tile có rỗng không
    public bool Empty()
    {
        if(tile == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //Kiểm tra cell có được ghi đè chưa(có tồn tại tile nào trên cell chưa)
    public bool Occupied()
    {
        if(tile != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
