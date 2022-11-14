﻿using UnityEngine;
using TMPro;

public class AlphaModifier : MonoBehaviour
{
    private bool awakeCalled = false;
    public SpriteRenderer spriteRenderer { get; set; }
    public TextMesh textMesh { get; set; }
    public TMP_Text tmpText { get; set; }

    public bool executeInAlphaModifierChildren = true;

    private bool executeAlpha = false;
    public float durationAlpha;
    private float startAlpha;
    private float finalAlpha;
    private float elapsedTimeAlpha;
    private float changeInValueAlpha;
    private EaseEquations.EaseFunctionDelegate easeFunctionAlpha;
    public delegate void AlphaEndedCallBackFunction();
    private AlphaEndedCallBackFunction AlphaEndedCallBack;
    private float currentAlpha;
    private Color color = new Color();

    public virtual void Awake()
    {
        if (!awakeCalled)
        {
            awakeCalled = true;
            spriteRenderer = GetComponent<SpriteRenderer>();
            textMesh = GetComponent<TextMesh>();
            tmpText = GetComponent<TMP_Text>();
        }
    }

    public void AlphaTo(float alpha,
        float time,
        EaseEquations.EaseFunctionDelegate easeFunction = null,
        AlphaEndedCallBackFunction EndCallBack = null)
    {
        AlphaStop();
        AlphaEndedCallBack = EndCallBack;
        if (time == 0)
        {
            if (spriteRenderer != null)
            {
                color.r = spriteRenderer.color.r;
                color.g = spriteRenderer.color.g;
                color.b = spriteRenderer.color.b;
                color.a = alpha;
                spriteRenderer.color = color;
            }
            else if (textMesh != null)
            {
                color.r = textMesh.color.r;
                color.g = textMesh.color.g;
                color.b = textMesh.color.b;
                color.a = alpha;
                textMesh.color = color;
            }
            else if (tmpText != null)
            {
                color.r = tmpText.color.r;
                color.g = tmpText.color.g;
                color.b = tmpText.color.b;
                color.a = alpha;
                tmpText.color = color;
            }
            OnFinished();
            return;
        }
        if (easeFunction == null)
        {
            easeFunction = EaseEquations.noEaseFunction;
        }
        finalAlpha = alpha;
        if (spriteRenderer != null)
        {
            startAlpha = spriteRenderer.color.a;
        }
        else if (textMesh != null)
        {
            startAlpha = textMesh.color.a;
        }
        else if (tmpText != null)
        {
            startAlpha = tmpText.color.a;
        }
        changeInValueAlpha = finalAlpha - startAlpha;
        executeAlpha = true;
        elapsedTimeAlpha = 0;
        durationAlpha = time;
        easeFunctionAlpha = easeFunction;
    }
    public void AlphaStop()
    {
        executeAlpha = false;
    }

    private void Update()
    {
        if (executeAlpha)
        {
            elapsedTimeAlpha += Time.deltaTime;
            currentAlpha = easeFunctionAlpha(
                changeInValueAlpha,
                elapsedTimeAlpha,
                durationAlpha,
                startAlpha
            );
            if (spriteRenderer != null)
            {
                color.r = spriteRenderer.color.r;
                color.g = spriteRenderer.color.g;
                color.b = spriteRenderer.color.b;
                color.a = currentAlpha;
                spriteRenderer.color = color;
            }
            else if (textMesh != null)
            {
                color.r = textMesh.color.r;
                color.g = textMesh.color.g;
                color.b = textMesh.color.b;
                color.a = currentAlpha;
                textMesh.color = color;
            }
            else if (tmpText != null)
            {
                color.r = tmpText.color.r;
                color.g = tmpText.color.g;
                color.b = tmpText.color.b;
                color.a = currentAlpha;
                tmpText.color = color;
            }
            if (elapsedTimeAlpha >= durationAlpha)
            {
                executeAlpha = false;
                if (spriteRenderer != null)
                {
                    color.r = spriteRenderer.color.r;
                    color.g = spriteRenderer.color.g;
                    color.b = spriteRenderer.color.b;
                    color.a = finalAlpha;
                    spriteRenderer.color = color;
                }
                else if (textMesh != null)
                {
                    color.r = textMesh.color.r;
                    color.g = textMesh.color.g;
                    color.b = textMesh.color.b;
                    color.a = finalAlpha;
                    textMesh.color = color;
                }
                else if (tmpText != null)
                {
                    color.r = tmpText.color.r;
                    color.g = tmpText.color.g;
                    color.b = tmpText.color.b;
                    color.a = finalAlpha;
                    tmpText.color = color;
                }
                OnFinished();
            }
        }
    }

    private void OnFinished()
    {
        AlphaEndedCallBack?.Invoke();
    }
}
