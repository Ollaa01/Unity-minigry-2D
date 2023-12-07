using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M_Tile : MonoBehaviour
{
    public int x;
    public int y;

    private M_Item _item;
    public M_Item Item
    {
        get => _item;

        set
        {
            if (_item == value) return;
            _item = value;
            icon.sprite = _item.sprite;
        }
    }

    public Image icon;

    public Button button;

    public M_Tile Left => x > 0 ? M_Board.Instance.tiles[x - 1, y] : null;
    public M_Tile Top => y > 0 ? M_Board.Instance.tiles[x, y - 1] : null;
    public M_Tile  Right => x < M_Board.Instance.Width - 1 ? M_Board.Instance.tiles[x + 1, y] : null;
    public M_Tile Bottom => y < M_Board.Instance.Height - 1 ? M_Board.Instance.tiles[x, y + 1] : null;

    public M_Tile[] Neighbours => new[]
    {
        Left,
        Top,
        Right,
        Bottom,
    };

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(() => M_Board.Instance.Select(this));
    }

    public List<M_Tile> GetConnectedTiles (List<M_Tile> exclude = null)
    {

        var result = new List<M_Tile> { this, };

        if(exclude == null)
        {
            exclude = new List<M_Tile> { this, };

        }
        else
        {
            exclude.Add(this);

        }

        foreach (var neighbour in Neighbours)  
        {

            if (neighbour == null || exclude.Contains(neighbour) || neighbour.Item != Item) { continue;  }

            result.AddRange(neighbour.GetConnectedTiles(exclude));

        }

        return result;
    }

}
