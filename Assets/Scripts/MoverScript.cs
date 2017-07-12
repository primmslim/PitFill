using UnityEngine;
using System.Collections;

public class MoverScript : MonoBehaviour {

    public GameObject board;
    private BoardScript boardScript;
    public GameObject CurrentTile;
    public BoardScript.Dir dir;
    public float MoveSpeed;
    private bool IsMoving;

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
            Vector2 TargetPos = new Vector2(CurrentTile.transform.position.x, CurrentTile.transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, TargetPos, MoveSpeed * Time.deltaTime);
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

            yield return new WaitForSeconds(MoveSpeed);
        }
        else
        {
            Debug.Log("You hit a wall!");
        }
        IsMoving = false;
    }
}
