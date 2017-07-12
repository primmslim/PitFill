using UnityEngine;
using System.Collections;
using System;

public class Tile : MonoBehaviour
{

    public int x;
    public int y;
    private BoardScript.TileStatus _status;
    public BoardScript.TileStatus Status
    {
        get { return _status; }
        set
        {
            _status = value;
        }
    }

    public delegate void StatusChangedEventHandler(object sender, EventArgs e);
    public event StatusChangedEventHandler StatusChanged;

    protected virtual void OnStatusChanged()
    {
        if (StatusChanged != null)
            StatusChanged(this, EventArgs.Empty);+
    }
}
