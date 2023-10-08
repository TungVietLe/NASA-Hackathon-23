using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public enum GameState
    {
        Welcome,
        FreePlay,
        Conclusion
    }

    public static GameStateManager Instance { get; private set; }

    public GameState CurrentState { get; set; }

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

    private void Start()
    {
        CurrentState = GameState.Welcome;
        PopupManager.Instance.InitPopupSequence("Welcome");
    }

    public void TransitionState(GameState to)
    {
        PopupManager.Instance.ClearPopups();

        switch (to)
        {

            case GameState.FreePlay:

                break;
            case GameState.Conclusion:
                PopupManager.Instance.InitPopupSequence("Conclusion", () =>
                {
                    // End Game
                    // https://discussions.unity.com/t/application-quit-not-working/130493
                    #if UNITY_EDITOR
                        // Application.Quit() does not work in the editor so
                        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
                        UnityEditor.EditorApplication.isPlaying = false;
                    #else
                        Application.Quit();
                    #endif
                });

                break;
        }
    }
}
