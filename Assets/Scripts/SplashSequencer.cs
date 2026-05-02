using System.Collections;
using UnityEngine;

public class SplashSequencer : MonoBehaviour
{
    [SerializeField] private float startDelay = 1;
    [SerializeField] private float interval = 2f;

    [Header("Components")]
    [SerializeField] private AudioSource soundEffect;
    [SerializeField] private SceneTransitioner transition;
    [SerializeField] private FaderView fade;

    private IEnumerator Start()
    {
        FaderView.OnFadeEnd += FadeEndCallback;

        yield return new WaitForSeconds(startDelay);
        fade.StartFade(FadeDirection.FadeIn);
    }

    private void OnDestroy()
    {
        FaderView.OnFadeEnd -= FadeEndCallback;
    }

    private void FadeEndCallback(FadeDirection type)
    {
        if (type == FadeDirection.FadeIn)
        {
            soundEffect.Play();
            Invoke(nameof(StartGame), interval);
        }
    }

    private void StartGame()
    {
        transition.LoadScene(SceneNames.GAME);
    }

    private void OnValidate() 
    {
        if (!soundEffect) soundEffect = GetComponent<AudioSource>();
    }
}