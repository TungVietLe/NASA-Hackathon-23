using System.Collections.Generic;
using UnityEngine;

public class TilesManager : MonoBehaviour
{
    public static TilesManager Instance { get; private set; }
    public Tile TileToPlace;
    private Dictionary<Vector2Int, GameObject> grid = new Dictionary<Vector2Int, GameObject>();
    [SerializeField] private ResourceManager resourceManager;
    [SerializeField] private Transform titan;
    [SerializeField] private MouseModeSwitcher mouseSwitcher;

    private Plane plane = new Plane(Vector3.up, 0);

    private void Awake()
    {
        if (Instance == null)
        {
            Instance= this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


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
        if (Input.GetMouseButtonDown(0) && mouseSwitcher.mode == MouseMode.Build && TileToPlace != null)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.normal);
                for(int i = 0;i < titan.childCount; i++)
                {
                    var child = titan.GetChild(i);
                    if (Vector3.Distance(child.transform.position, hit.point) < 0.5f) return ;
                }


                var newTile = Instantiate(TileToPlace, titan);
                newTile.transform.position = hit.point;
                newTile.transform.up = hit.normal;

                resourceManager.Oxygen += newTile.oxygen;
                resourceManager.Temperature += newTile.temperature;
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
