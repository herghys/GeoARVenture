using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;

using DG.Tweening;

public class SplashScreenManager : MonoBehaviour
{
    [SerializeField] Transform[] transformToAnimate;
    [SerializeField] Slider progressSlider;
    [SerializeField] TextMeshProUGUI progressText;

    [SerializeField] string scene;
    AsyncOperation loadOp;
    float activeTime = 0f;

    private void Start()
    {
        StartLoading();
    }

    private void Update()
    {
        activeTime += Time.deltaTime;
        var progress = Mathf.Clamp01(activeTime / 5);

        print(progress);

        if (progress < 1f)
        {
            progressSlider.value = progress;
            progressText.text = $"{(progress * 100):0.0}%";
        }

        if (progress == 1f )
        {
            print("Load");
            loadOp.allowSceneActivation = true;
        }
    }
    async void StartLoading()
    {
        loadOp = SceneManager.LoadSceneAsync(scene);
        loadOp.allowSceneActivation = false;
        for (int i = 0; i < transformToAnimate.Length; i++)
        {
            AnimateLogoAsync(i, 2f);
            await Task.Yield();
        }
        await Task.Yield();
    }

    private async void AnimateLogoAsync(int index, float duration)
    {
        var tween = transformToAnimate[index].DOScale(Vector3.one, duration);
        tween.SetEase(Ease.OutBounce);
        await tween.AsyncWaitForCompletion();
    }
}