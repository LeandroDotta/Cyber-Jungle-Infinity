using UnityEditor;
using UnityEngine;

public class MainMenuPresenter : MonoBehaviour
{
    private const string YOUTUBE_CHANNEL_URL = "https://www.youtube.com/@LeandroDotta";

    [SerializeField] private MainMenuView view;
    
    private SceneTransitioner transition;

    private void Awake() 
    {
        if (!view) view = GetComponent<MainMenuView>();
        transition = FindFirstObjectByType<SceneTransitioner>();
    }

    private void OnEnable()
    {
        view.ButtonPlay.onClick.AddListener(ClickPlay);
        view.ButtonExit.onClick.AddListener(ClickExit);
        view.ButtonYoutube.onClick.AddListener(ClickYoutube);
    }

    private void OnDisable()
    {
        view.ButtonPlay.onClick.RemoveListener(ClickPlay);
        view.ButtonExit.onClick.RemoveListener(ClickExit);
        view.ButtonYoutube.onClick.RemoveListener(ClickYoutube);
    }

    private void ClickPlay()
    {
        transition.LoadScene(SceneNames.LEVEL_PREFIX+"1");
        view.DisableAllButtons();
    }

    private void ClickExit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        Application.Quit();
        
    }
    
    private void ClickYoutube()
    {
        Application.OpenURL(YOUTUBE_CHANNEL_URL);
    }
}
