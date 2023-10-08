using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupRunner : MonoBehaviour
{
    public TextMeshProUGUI Title;
    public TextMeshProUGUI Body;
    public Button continueButton;

    public void InitText(string title, string body)
    {
        Title.text = title;
        Body.text = body;
    }
}
