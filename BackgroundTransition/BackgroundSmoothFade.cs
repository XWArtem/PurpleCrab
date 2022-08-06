using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundSmoothFade : MonoBehaviour
{
    private float _startAlpha = 0f;
    private float _finalAlpha = 150f;
    private Image _greyBackground;
    private Color _tempColor;

    private void OnEnable()
    {
        UIManager.BackgroundFadeAction += FadeBackground;
    }
    private void OnDisable()
    {
        UIManager.BackgroundFadeAction -= FadeBackground;
    }
    private void Start()
    {
        _greyBackground = GetComponent<Image>();
        _tempColor = _greyBackground.color;
        _tempColor.a = _startAlpha;
    }
    private void FadeBackground(bool fadeToZero)
    {
        if (fadeToZero)
        {
            _startAlpha = 0.5f;
            _finalAlpha = 0f;
        }
        else
        {
            _startAlpha = 0f;
            _finalAlpha = 0.5f;
        }
        _tempColor.a = _startAlpha;
        _greyBackground.color = _tempColor;
        StartCoroutine(FadeBackgroundCoroutine());
    }
    IEnumerator FadeBackgroundCoroutine()
    {
        for (float a = _startAlpha; a <= _finalAlpha; a += 0.07f)
        {
            _tempColor.a = a;
            _greyBackground.color = _tempColor;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
