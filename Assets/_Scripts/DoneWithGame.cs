using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class DoneWithGame : MonoBehaviour
{
    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => { GameStateManager.Instance.TransitionState(GameStateManager.GameState.Conclusion); });
    }
}
