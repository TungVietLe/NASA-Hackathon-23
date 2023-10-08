using TMPro;
using UnityEngine;

public class TileSelectBoard : MonoBehaviour
{
    public TextMeshProUGUI currentTileTMP;
    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);    
    }
    public void Toggle()
    {
        if (gameObject.activeSelf)
        {
            Close();
        }
        else Open();
    }

    private void Awake()
    {
        Close();
    }
}
