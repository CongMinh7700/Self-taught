using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBoard : MonoBehaviour
{
    public Tile tilePrefabs;
    public TileState[] tileStates;
    private TileGrid _grid;
    public GameManager gameManager;
    private List<Tile> _tiles;
    private bool _waiting;
    private void Awake()
    {
        _grid = GetComponentInChildren<TileGrid>();
        _tiles = new List<Tile>(16);

    }
    //xóa tất cả các tile
    public void ClearBoard()
    {
        foreach(var cell in _grid.cells)
        {
            cell.tile = null;
        }
        foreach(var tile in _tiles)
        {
            Destroy(tile.gameObject);
        }
        _tiles.Clear();
    }
    //Tạo mới 1 tile theo vị trí random
    public  void CreateTile()
    {
        Tile tile = Instantiate(tilePrefabs, _grid.transform);
        tile.SetState(tileStates[0], 2);
        tile.Spawn(_grid.GetRandomEmptyCell());
        _tiles.Add(tile);

    }
    private void Update()
    {
        if (!_waiting)
        {
            MoveControl();
        }
       
    }
    //Thực hiện thao tác điều khiển của game
    private void MoveControl()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveTiles(Vector2Int.up, 0, 1, 1, 1);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveTiles(Vector2Int.left, 1, 1, 0, 1);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveTiles(Vector2Int.down, 0, 1, _grid.height - 2, -1);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveTiles(Vector2Int.right, _grid.width - 2, -1, 0, 1);
        }
    }
    //Move nhiều tiles 
    private void MoveTiles(Vector2Int direction, int startX, int incrementX, int startY, int incrementY)
    {
        bool changed = false;
        for (int x = startX; x >= 0 && x < _grid.width; x += incrementX)
        {
            for (int y = startY; y >= 0 && y < _grid.height; y += incrementY)
            {
                TileCell cell = _grid.GetCell(x, y);
                if (cell.Occupied())
                {
                    //phép or sẽ trả về true false và được gán cho changed
                   changed |= MoveTile(cell.tile, direction);
                }
            }
        }
        if(changed)
        {
            StartCoroutine(WaitForChange());
        }
    }
    //Move từng tile
    private bool MoveTile(Tile tile, Vector2Int direction)
    {
        TileCell newCell = null;
        //lấy ô liền kề
        //nếu có thì mới thực hiện 
        TileCell adjacent = _grid.GetAdjacentCell(tile.cell, direction);
        while (adjacent != null)
        {
            //nếu ô liền kề có tile tiến hành merge
            if (adjacent.Occupied())
            {
                if (CanMerge(tile, adjacent.tile))
                {
                    Merge(tile, adjacent.tile);
                }
                break;
            }
            newCell = adjacent;
            adjacent = _grid.GetAdjacentCell(adjacent, direction);
        }
        if (newCell != null)
        {
            tile.MoveTo(newCell);
            //StartCoroutine(WaitForChange());
            return true;

        }
        return false;
    }

    private IEnumerator WaitForChange()
    {
        _waiting = true;
        yield return new WaitForSeconds(0.1f);
        _waiting = false;
        foreach(var tile in _tiles)
        {
            tile.locked = false;
        }
        //tạo tile mới khi mà số lượng tile < số ô trong grid(16 ô)
        if (_tiles.Count != _grid.size)
        {
            CreateTile();
        }

        //check game over
        if (CheckForGameOver())
        {
            gameManager.GameOver();
        }
    }
    // kiểm tra điều kiện để merge
    private bool CanMerge(Tile a,Tile b)
    {
        return a.value == b.value && !b.locked;

    }
    public void Merge(Tile a,Tile b)
    {
        _tiles.Remove(a);
        a.Merge(b.cell);
        //dùng để lấy trạng thái cảu tile(màu sắc ,giá trị)
        int index = Math.Clamp(IndexOf(b.state)+1,0,tileStates.Length-1);
        int value = b.value * 2;
        b.SetState(tileStates[index], value );
        gameManager.IncreaseScore(value);
    }
    //lấy vị trí trạng thái của tile có 11 giá trị từ 0->11
    private int IndexOf(TileState state) 
    {
        for(int i = 0; i < tileStates.Length; i++)
        {
            if(state == tileStates[i])
            {
                return i;
            }
        }
        return -1;
    }
    //Check game over
    private bool CheckForGameOver()
    {
        if(_tiles.Count != _grid.size)
        {
            return false;
        }
        foreach(var tile in _tiles)
        {
            TileCell up = _grid.GetAdjacentCell(tile.cell, Vector2Int.up);
            TileCell down = _grid.GetAdjacentCell(tile.cell, Vector2Int.down);
            TileCell right = _grid.GetAdjacentCell(tile.cell, Vector2Int.right);
            TileCell left = _grid.GetAdjacentCell(tile.cell, Vector2Int.left);
            //kiểm tra còn move dc hay không
            if(up != null && CanMerge(tile,up.tile))
            {
                return false;
            } 
            if(left != null && CanMerge(tile,left.tile))
            {
                return false;
            }  
            if(right != null && CanMerge(tile,right.tile))
            {
                return false;
            } 
            if(down != null && CanMerge(tile,down.tile))
            {
                return false;
            }
           
        }
        return true;
    }
}
