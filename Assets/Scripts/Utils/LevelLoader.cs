using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Slider slider;
    public Text teks;

    public float delay = 3;

    public float ActiveTime = 0f;

    void Awake()
    {
        Debug.Log("AAA");
        //slider.value = 0f;

        //StartCoroutine(LoadAsynchronously(1, delay));
    }

    void Update()
    {
        ActiveTime += Time.deltaTime;
        var percent = ActiveTime / 5;
        Debug.Log(percent);

        slider.value = percent;

        if (Mathf.Clamp01(percent) == 1f)
        {
            SceneManager.LoadScene(1);
        }
        //slider.value = Time.deltaTime;
    }

    public void LoadLevel (int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex, delay));
        Debug.Log("AAA");
    }

    IEnumerator LoadAsynchronously (int sceneIndex, float delay)
    {
        yield return new WaitForSeconds(2);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            Debug.Log(progress);

//            slider.value = Time.deltaTime;

            yield return null;
        }
    }
}
