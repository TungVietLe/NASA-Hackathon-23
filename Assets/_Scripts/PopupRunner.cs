using UnityEngine;
using TMPro;

public class PopupRunner : MonoBehaviour
{
    public TextMeshProUGUI Title;
    public TextMeshProUGUI Body;

    public void InitText(string title, string body)
    {
        Title.text = title;
        Body.text = body;
    }
}
