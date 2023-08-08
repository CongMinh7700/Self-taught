using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoBehaviour
{
    public TileRow[] rows { get; private set; }
    public TileCell[] cells { get; private set; }
    //Kiểu trả về 
    public int size => cells.Length;
    public int height => rows.Length;
    public int width => size / height;
    private void Awake()
    {
        //truy cập các thành phần con của rows,cells có trong tile Grid
        rows = GetComponentsInChildren<TileRow>();
        cells = GetComponentsInChildren<TileCell>();
    }
    //thiết lập tọa độ cho các cells
    private void Start()
    {
        
        for (int y = 0; y < rows.Length; y++)
        {
            //row[y].cells là lấy x theo chiều ngang và y theo chiều dọc
            //có 4 rows thì rows.length  sẽ là số rows 
            //vì ma trận 4*4 nên số nên 1 row có 4 cells 
            
            for (int x = 0; x < rows[y].cells.Length; x++)
            {
                rows[y].cells[x].coordinates = new Vector2Int(x, y);
            }
        }

    }
    //Lấy cell trống
    public TileCell GetRandomEmptyCell()
    {
        int index = Random.Range(0, cells.Length);
        int startingIndex = index;

        //Kiểm tra tính hợp lệ để kiểm tra cell trống
        while (cells[index].Occupied())
        {
            index++;
            if (index >= cells.Length)
            {
                index = 0;
            }
            if (index == startingIndex)
            {
                return null;
            }
        }
        return cells[index];
    }
    //Lấy vị trí theo x,y
    //PHOLYMORPHISM
    public TileCell GetCell(int x, int y)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            return rows[y].cells[x];
        }   
        else
        {
            return null;
        }

    }
    //lấy vị trí theo tọa độ
    public TileCell GetCell(Vector2Int coordinates)
    {
        return GetCell(coordinates.x, coordinates.y);
    }
    //lấy vị trí của ô kế bên
    public TileCell GetAdjacentCell(TileCell cell, Vector2Int direction)
    {
        Vector2Int coordinates = cell.coordinates;
        coordinates.x += direction.x;
        coordinates.y -= direction.y;
        return GetCell(coordinates);
    }

}
