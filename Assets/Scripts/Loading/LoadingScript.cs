using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

using System.Threading.Tasks;
using System.Collections;

public class LoadingScript : MonoBehaviour
{
    private static LoadingScript Instance;
    public static LoadingScript SharedInstance { get => Instance; }

    [SerializeField] Slider progressBar;
    [SerializeField] TextMeshProUGUI progressText;

    AsyncOperation asyncScene = null;

    private void Awake()
    {
        if (Instance != null) return;
        Instance = this;
    }

    public async void LoadScene(string sceneName)
    {
        var loadOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncScene = loadOperation;
        do
        {
            await Task.Delay(10);
            float progress = Mathf.Clamp01(loadOperation.progress / 0.9f);
            UpdateProgressBar(progress);
        } while (!loadOperation.isDone);
        await Task.Delay(10);
    }

    public IEnumerator ILoadScene(string sceneName, bool allowActivation = true)
    {
        var loadOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncScene = loadOperation;
        while (!loadOperation.isDone)
        {
            float progress = Mathf.Clamp01(loadOperation.progress / 0.9f);
            UpdateProgressBar(progress);
            yield return null;
        }
    }

    public void ActivateScene()
    {
        if (asyncScene != null)
        asyncScene.allowSceneActivation = true;
    }

    private void UpdateProgressBar(float progress)
    {
        progressBar.value = progress;
        progressText.text = $"{progress * 100}%";
    }
}