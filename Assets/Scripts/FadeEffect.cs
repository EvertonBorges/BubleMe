using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class FadeEffect : MonoBehaviour
{

    [Header("Configuration")]
    [SerializeField]
    private bool startActive = false;
    
    [Header("Fade Controls")]
    [Range(0.25f, 5f)] [SerializeField] private float fadeInTime = 1f;
    [Range(0.25f, 5f)] [SerializeField] private float fadeOutTime = 1f;

    [Header("Alpha Controls")]
    [SerializeField, Range(0f, 1f)] private float alphaStart = 0f;
    [SerializeField, Range(0f, 1f)] private float alphaEnd = 1f;

    private bool _isShowing = false;
    private bool _isFading = false;
    public bool IsShowing => _isShowing;
    
    private CanvasGroup canvas = null;
    private Coroutine _effectCoroutine = null;

    void Awake() 
    {
        canvas = GetComponent<CanvasGroup>();

        gameObject.SetActive(startActive);
        canvas.alpha = startActive ? alphaEnd : alphaStart;
        _isShowing = startActive;
    }

    public void FadeIn(Action callback = null)
    {
        if (!_isShowing && !_isFading)
        {
            Effect(true, callback);
        }
    }

    public void ShowForced()
    {
        _isShowing = true;
        _isFading = false;

        gameObject.SetActive(true);
        canvas.interactable = true;
        canvas.alpha = alphaEnd;
    }

    public void FadeOut(Action callback = null)
    {
        if (_isShowing && !_isFading)
        {
            Effect(false, callback);
        }
    }

    public void HideForced()
    {
        _isShowing = false;
        _isFading = false;

        canvas.alpha = alphaStart;
        canvas.interactable = false;
        gameObject.SetActive(false);
    }

    private void Effect(bool fadeIn = true, Action callback = null)
    {
        if (!_isFading)
        {
            if (_effectCoroutine != null) 
                MonoBehaviorHelper.StopCoroutine(_effectCoroutine);
                
            _effectCoroutine = MonoBehaviorHelper.StartCoroutine(EffectCoroutine(fadeIn, callback));
        }
    }

    private IEnumerator EffectCoroutine(bool fadeIn, Action callback = null)
    {
        gameObject.SetActive(true);

        _isFading = true;
        canvas.interactable = true;

        float startAlpha = fadeIn ? alphaStart : alphaEnd;
        float endAlpha = fadeIn ? alphaEnd : alphaStart;

        if (canvas.alpha > 0f && canvas.alpha < 1f)
            startAlpha = canvas.alpha;
        
        float animationTime = fadeIn ? fadeInTime : fadeOutTime;
        float currentTime = 0f;
        if (fadeIn)
        {
            if (endAlpha == 0f)
                currentTime = animationTime;
            else
                currentTime = startAlpha / endAlpha * animationTime;

        }
        else
        {
            if (startAlpha == 0f)
                currentTime = animationTime;
            else
                currentTime = endAlpha / startAlpha * animationTime;
        }

        while(currentTime < animationTime)
        {
            currentTime += Time.deltaTime;
            float proportionTime = currentTime / animationTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, proportionTime);
            canvas.alpha = alpha;

            yield return null;
        }

        if (!fadeIn) 
        {
            canvas.interactable = false;
            gameObject.SetActive(false);
        }

        canvas.alpha = endAlpha;
        _isShowing = fadeIn;
        _isFading = false;
        callback?.Invoke();

        yield return null;
    }

}
