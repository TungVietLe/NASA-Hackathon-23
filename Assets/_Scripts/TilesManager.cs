using System.Collections.Generic;
using UnityEngine;

public class TilesManager : MonoBehaviour
{
    private Dictionary<Vector2Int, Tile> grid = new Dictionary<Vector2Int, Tile>();
    [SerializeField] private Tile testTile;
    [SerializeField] private Vector2Int pos;

    public void PlaceTile(Tile tile, Vector2Int pos)
    {
        grid[pos] = tile;
        var newTile = Instantiate(tile, this.transform);
        newTile.transform.localPosition = new Vector3(pos.x + 0.5f, 0, pos.y + 0.5f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlaceTile(testTile, pos);
        }
    }
}
