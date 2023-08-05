using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBoard : MonoBehaviour
{
    public Tile tilePrefabs;
    public TileState[] tileStates;
    private TileGrid grid;
    public GameManager gameManager;
    private List<Tile> tiles;
    private bool waiting;
    private void Awake()
    {
        grid = GetComponentInChildren<TileGrid>();
        tiles = new List<Tile>(16);

    }
    public void ClearBoard()
    {
        foreach(var cell in grid.cells)
        {
            cell.tile = null;
        }
        foreach(var tile in tiles)
        {
            Destroy(tile.gameObject);
        }
        tiles.Clear();
    }

    public  void CreateTile()
    {
        Tile tile = Instantiate(tilePrefabs, grid.transform);
        tile.SetState(tileStates[0], 2);
        tile.Spawn(grid.GetRandomEmptyCell());
        tiles.Add(tile);

    }
    private void Update()
    {
        if (!waiting)
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
            MoveTiles(Vector2Int.down, 0, 1, grid.height - 2, -1);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveTiles(Vector2Int.right, grid.width - 2, -1, 0, 1);
        }
        }
       
    }
    private void MoveTiles(Vector2Int direction, int startX, int incrementX, int startY, int incrementY)
    {
        bool changed = false;
        for (int x = startX; x >= 0 && x < grid.width; x += incrementX)
        {
            for (int y = startY; y >= 0 && y < grid.height; y += incrementY)
            {
                TileCell cell = grid.GetCell(x, y);
                if (cell.Occupied())
                {
                   changed |= MoveTile(cell.tile, direction);
                }
            }
        }
        if(changed)
        {
            StartCoroutine(WaitForChange());
        }
    }
    private bool MoveTile(Tile tile, Vector2Int direction)
    {
        TileCell newCell = null;
        TileCell adjacent = grid.GetAdjacentCell(tile.cell, direction);
        while (adjacent != null)
        {
            if (adjacent.Occupied())
            {
                if (CanMerge(tile, adjacent.tile))
                {
                    Merge(tile, adjacent.tile);
                }
                break;
            }
            newCell = adjacent;
            adjacent = grid.GetAdjacentCell(adjacent, direction);
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
        waiting = true;
        yield return new WaitForSeconds(0.1f);
        waiting = false;
        foreach(var tile in tiles)
        {
            tile.locked = false;
        }
        //create New tile
        if (tiles.Count != grid.size)
        {
            CreateTile();
        }

        //check game over
        if (CheckForGameOver())
        {
            gameManager.GameOver();
        }
    }
    private bool CanMerge(Tile a,Tile b)
    {
        return a.value == b.value && !b.locked;

    }
    public void Merge(Tile a,Tile b)
    {
        tiles.Remove(a);
        a.Merge(b.cell);
        int index = Math.Clamp(  IndexOf(b.state)+1,0,tileStates.Length-1);
        int value = b.value * 2;
        b.SetState(tileStates[index], value );
        gameManager.IncreaseScore(value);
    }
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
    private bool CheckForGameOver()
    {
        if(tiles.Count != grid.size)
        {
            return false;
        }
        foreach(var tile in tiles)
        {
            TileCell up = grid.GetAdjacentCell(tile.cell, Vector2Int.up);
            TileCell down = grid.GetAdjacentCell(tile.cell, Vector2Int.down);
            TileCell right = grid.GetAdjacentCell(tile.cell, Vector2Int.right);
            TileCell left = grid.GetAdjacentCell(tile.cell, Vector2Int.left);
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
