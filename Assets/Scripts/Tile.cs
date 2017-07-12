using UnityEngine;
using System.Collections;
using System;

public class Tile : MonoBehaviour
{
    public enum TileStatus { Available, Blocked, Used }
    public int x;
    public int y;
    public Color AvailableColour;
    public Color BlockedColour;
    public Color UsedColour;
    private TileStatus _status;
    public TileStatus Status
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

            StatusChanged(this, EventArgs.Empty);
    }

    public void ChangeStatus(TileStatus Status)
    {
        Color colour = new Color();
        switch (Status)
        {
            case TileStatus.Available:
                colour = AvailableColour;
                break;
            case TileStatus.Blocked:
                colour = BlockedColour;
                break;
            case TileStatus.Used:
                colour = UsedColour;
                break;
                       
        }
        _status = Status;
        transform.FindChild("Inner").GetComponent<SpriteRenderer>().color = colour;
        OnStatusChanged();
    }
}
