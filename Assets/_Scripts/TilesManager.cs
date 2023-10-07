using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class TilesManager : MonoBehaviour
{
    private Dictionary<Vector2Int, GameObject> grid = new Dictionary<Vector2Int, GameObject>();
    [SerializeField] private Tile tileToPlaceTest;
    [SerializeField] private Transform parent;

    private Plane plane = new Plane(Vector3.up, 0);
  
        

    public void PlaceTile(Tile tile, Vector2Int pos)
    {
        DeleteTile(pos);
        var newTile = Instantiate(tile, this.transform);
        newTile.transform.localPosition = new Vector3(pos.x, 0, pos.y);
        grid[pos] = newTile.gameObject;
    }
    public void DeleteTile(Vector2Int pos)
    {
        if (!grid.ContainsKey(pos)) return;
        Destroy(grid[pos]);
        grid[pos] = null;
    }

    private void Update()
    {
        HandleMouseInput();
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.normal);
                for(int i = 0;i < parent.childCount; i++)
                {
                    var child = parent.GetChild(i);
                    if (Vector3.Distance(child.transform.position, hit.point) < 0.5f) return ;
                }


                var newTile = Instantiate(tileToPlaceTest, parent);
                newTile.transform.position = hit.point;
                newTile.transform.up = hit.normal;
                newTile.transform.localScale /= 4;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float distance;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out distance))
            {
                var worldPosition = ray.GetPoint(distance);
                Destroy(grid[new Vector2Int(Mathf.RoundToInt(worldPosition.x), Mathf.RoundToInt(worldPosition.z))]);
            }
        }
    }

}
