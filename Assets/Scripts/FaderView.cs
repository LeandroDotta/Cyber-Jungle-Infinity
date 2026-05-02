using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum FadeDirection
{
    FadeIn, FadeOut
}

public class FaderView : MonoBehaviour
{
    [Header("Colors")]
    [SerializeField] private Color from = Color.black;
    [SerializeField] private Color to = Color.clear;

    [Header("Transition")]
    [SerializeField] private float fadeInDuration = 0.5f;
    [SerializeField] private AnimationCurve fadeInCurve;
    [SerializeField] private float fadeOutDuration = 0.5f;
    [SerializeField] private AnimationCurve fadeOutCurve;

    [Header("Components")]
    [SerializeField] private Image targetImage;

    public static event UnityAction<FadeDirection> OnFadeStart;
    public static event UnityAction<FadeDirection> OnFadeEnd;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void StartFade(FadeDirection direction)
    {
        StopAllCoroutines();
        StartCoroutine(FadeCoroutine(direction));
    }

    private IEnumerator FadeCoroutine(FadeDirection direction)
    {
        targetImage.enabled = true;
        OnFadeStart?.Invoke(direction);

        if (direction == FadeDirection.FadeIn)
            yield return ColorTransitionCoroutine(from, to, fadeInDuration, fadeInCurve);
        else
            yield return ColorTransitionCoroutine(to, from, fadeOutDuration, fadeOutCurve);

        OnFadeEnd?.Invoke(direction);
    }

    private IEnumerator ColorTransitionCoroutine(Color startColor, Color endColor, float duration, AnimationCurve curve)
    {
        float elapsed = 0;
        targetImage.color = startColor;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float time = Mathf.Clamp01(elapsed / duration);
            float curveTime = curve.Evaluate(time);

            targetImage.color = Color.Lerp(startColor, endColor, curveTime);

            yield return null;
        }

        targetImage.color = endColor;
    }

    private void OnValidate()
    {
        if (!targetImage) targetImage = GetComponentInChildren<Image>();
    }
}
