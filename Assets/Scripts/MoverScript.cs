using UnityEngine;
using System.Collections;

public class MoverScript : MonoBehaviour {

    public GameObject board;
    private BoardScript boardScript;
    public GameObject CurrentTile;
    public BoardScript.Dir dir;
    public float MoveTime;
    public float MinSpeed;
    public float MaxSpeed;
    private bool IsMoving;
    public float StartTime;
    private void Awake()
    {
        boardScript = board.GetComponent<BoardScript>();

    }

    private void Start()
    {
        CurrentTile = boardScript.Tiles[0].gameObject;
        transform.position = CurrentTile.transform.position;
    }

    private void Update()
    {
        UpdateDir();
        if (!IsMoving)
        {
            IsMoving = true;

            StartCoroutine(MoveToNext());
        }
        else
        {
            //Move towards the current tile
            Vector2 TargetPos = new Vector2(CurrentTile.transform.position.x, CurrentTile.transform.position.y);
            //transform.position = Vector2.MoveTowards(transform.position, TargetPos, MoveSpeed * Time.deltaTime);
            float t = (Time.time - StartTime) / MoveTime;
            float MoveDelta = Mathf.SmoothStep(MinSpeed, MaxSpeed, t);
            Vector2 NewPos = Vector2.Lerp(transform.position ,TargetPos,MoveDelta);
            transform.position = NewPos;
        }

        

    }

    private void UpdateDir()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow)) dir = BoardScript.Dir.Down;
        if (Input.GetKeyDown(KeyCode.UpArrow)) dir = BoardScript.Dir.Up;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) dir = BoardScript.Dir.Left;
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            dir = BoardScript.Dir.Right;
        }

    }

    private  IEnumerator MoveToNext()
    {

        Tile tile = CurrentTile.GetComponent<Tile>();
        
        var t = boardScript.MoveNext(tile, dir);
        if (t != null)
        {
            CurrentTile = t.gameObject;
            StartTime = Time.time;
            
            t.ChangeStatus(Tile.TileStatus.Used);
            yield return new WaitForSeconds(MoveTime);
        }
        else
        {
            Debug.Log("You hit a wall!");
        }
        IsMoving = false;
    }
}
