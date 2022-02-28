using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

using DG.Tweening;

public class SplashScreenManager : MonoBehaviour
{
    [SerializeField] string scene;
    [SerializeField] float totalTime = 2f;
    [SerializeField] SceneFader fader;
    AsyncOperation loadOp;
    float activeTime = 0f;

    private void Start()
    {
        StartCoroutine(StartLoading());
    }

    private void Update()
    {
        activeTime += Time.deltaTime;
        var progress = Mathf.Clamp01(activeTime / totalTime);

        print(progress);

        if (progress == 1f )
        {
            fader.FadeTo(scene, loadOp);
        }
    }

    IEnumerator StartLoading()
    {
        loadOp = SceneManager.LoadSceneAsync(scene);
        loadOp.allowSceneActivation = false;
        yield return null;
    }
}