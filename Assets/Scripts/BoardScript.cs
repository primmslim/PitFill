using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class BoardScript : MonoBehaviour {

    public int Height, Width;
    public GameObject SourceTile;
    public Camera camera;
  
    public List<Tile> Tiles;
    public int[,] BlockedTiles;

    public enum Dir { Up,Down,Left,Right}
    public enum TileStatus { Available, Blocked, Used}
    private void Awake()
    {
        SetTiles();

        SetCamera();

    }

    private void SetCamera()
    {
        //set the camera

        Vector3 CameraPos = new Vector3(Width / 2, Height / 2, -10);

        camera.transform.position = CameraPos;
        camera.orthographicSize = Height / 2;
    }

    private void SetTiles()
    {
        //create the tiles
        float TileWIdth = SourceTile.transform.localScale.x;

        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                Vector2 pos = new Vector2();
                pos.y = (i * TileWIdth) + 0.5f;
                pos.x = (j * TileWIdth) + 0.5f;

                GameObject t = (GameObject)Instantiate(SourceTile, pos, SourceTile.transform.rotation, this.transform);
                t.name = string.Format("X{0}Y{1}", j, i);
                Tile tile = t.GetComponent<Tile>();

                Tiles.Add(tile);
                tile.x = j;
                tile.y = i;
            }
        }
    }

    public Tile MoveNext(Tile tile, Dir dir)
    {
        //search tiles for next increment
        int targetX, targetY;
        targetX = tile.x;
        targetY = tile.y;
        
        switch (dir)
        {
            case Dir.Down:
                targetY--;
                break;
            case Dir.Left:
                targetX--;
                break;
            case Dir.Right:
                targetX++;
                break;
            case Dir.Up:
                targetY++;
                break;
            default: break;
        }

        var res = Tiles.Find(t => t.x == targetX && t.y == targetY);
        return res;

        
    }
}
