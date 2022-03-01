using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ARMath.Managers;
using System;

public class SceneFader : MonoBehaviour
{
    public Image img;
    public AnimationCurve curve;
    public bool fadeInEnter = true;

    private void OnEnable()
    {
        UIManager.SharedInstance.StartFade += StartFade;
    }

    private void OnDisable()
    {
        UIManager.SharedInstance.StartFade -= StartFade;
    }

    void Start()
    {
        if(fadeInEnter)
        StartCoroutine(FadeIn());
    }
    #region Fade Out
    private void StartFade(AsyncOperation op)
    {
        StartCoroutine(FadeOut(op));
    }
    public void FadeTo(AsyncOperation op)
    {
        StartCoroutine(FadeOut(op));
    }

    IEnumerator FadeOut(AsyncOperation op)
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
        op.allowSceneActivation = true;
    }

    #endregion

    #region Fade In
    IEnumerator FadeIn()
    {
        float t = 1f;

        while (t > 0f)
        {
            t -= Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
    } 
    #endregion
}