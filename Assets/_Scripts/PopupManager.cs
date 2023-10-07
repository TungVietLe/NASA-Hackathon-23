using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public GameObject TextPopup;
    public static PopupManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Run the tutorial sequence of text boxes
    /// </summary>
    public static void Tutorial()
    {

    }
}
