using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TileOption : MonoBehaviour
{
    [SerializeField] Tile tileToHold;

    private void Awake()
    {
        GetComponent<Image>().sprite = tileToHold.thumbnail;
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = tileToHold.name;
    }

    public void SelectThisTile()
    {
        TilesManager.Instance.TileToPlace = tileToHold;
        var board = GetComponentInParent<TileSelectBoard>();
        board.Close();
        board.currentTileTMP.text = tileToHold.name;
    }
}
