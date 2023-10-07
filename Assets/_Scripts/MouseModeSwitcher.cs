using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MouseModeSwitcher : MonoBehaviour
{
    public MouseMode mode = MouseMode.Build;
    [SerializeField] Sprite viewIcon;
    [SerializeField] Sprite buildIcon;

    public void Toggle()
    {
        print("toggle cam mode");
        GetComponent<Image>().sprite = mode == MouseMode.Build ? viewIcon: buildIcon;
        mode = mode == MouseMode.Build ? MouseMode.View : MouseMode.Build;
    }
}

public enum MouseMode { Build, View }
