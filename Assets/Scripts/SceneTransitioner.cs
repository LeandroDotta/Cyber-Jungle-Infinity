using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour
{
    [SerializeField] private FaderView fade;

    private string sceneName;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        FaderView.OnFadeEnd += FadeEndCallback;
    }
    
    private void OnDestroy()
    {
        FaderView.OnFadeEnd -= FadeEndCallback;
    }

    public void LoadScene(string sceneName)
    {
        this.sceneName = sceneName;

        if (!fade)
        {
            LoadWithoutTransition();
            return;
        }

        LoadWithTransition();
    }

    private void FadeEndCallback(FadeDirection type)
    {
        if (type == FadeDirection.FadeOut)
        {
            StartCoroutine(SceneTransitionCoroutine());
        }
    }
    
    private IEnumerator SceneTransitionCoroutine()
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName);
        yield return loadOperation;

        fade.StartFade(FadeDirection.FadeIn);

        sceneName = null;
    }

    private void LoadWithTransition()
    {
        fade.StartFade(FadeDirection.FadeOut);
    }
    
    private void LoadWithoutTransition()
    {
        SceneManager.LoadScene(sceneName);
        sceneName = null;
    }
}
