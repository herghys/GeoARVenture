using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SplashScreenManager : MonoBehaviour
{
    [Header("Splash Loader")]
    [SerializeField] string scene;
    [SerializeField] float totalTime = 2f;
    AsyncOperation loadOp;
    float activeTime = 0f;
    
    [Header("Fader")]
    [SerializeField] Image img;
    [SerializeField] AnimationCurve curve;
    [SerializeField] float curveTime = 1f;
    private void Start()
    {
        StartCoroutine(StartLoading());
    }

    private void Update()
    {
        activeTime += Time.deltaTime;
        var progress = Mathf.Clamp01(activeTime / totalTime);

        if (progress == 1f )
        {
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeOut()
    {
        float t = 0f;

        while (t < curveTime)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
        loadOp.allowSceneActivation = true;
    }

    IEnumerator StartLoading()
    {
        loadOp = SceneManager.LoadSceneAsync(scene);
        loadOp.allowSceneActivation = false;
        yield return null;
    }
}