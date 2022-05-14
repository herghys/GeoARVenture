using UnityEngine;
using UnityEngine.SceneManagement;
using ARMath.UI;
using System.Collections;
using System.Threading.Tasks;

namespace ARMath.Managers
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] MainMenuUIManager ui;
        [SerializeField] SceneFader fader;

        AsyncOperation loadOp;

        private void Awake()
        {
            if(ui == null)
                ui = GetComponent<MainMenuUIManager>();
        }

        #region Context Menu Control
        public void OpenContextMenu(UIWindow window)
        {
            ui.OpenUI(window);
        }

        public void CloseContextMenu(UIWindow window)
        {
            ui.CloseUI(window);
        }
        #endregion

        #region Scene Loader
        public void LoadScene(string scene)
        {
            ui.StartLoadLevel();
            StartCoroutine(Load(scene));
        }

        IEnumerator Load(string scene)
        {
            loadOp = SceneManager.LoadSceneAsync(scene);
            loadOp.allowSceneActivation = false;
            while (!loadOp.isDone)
            {
                float progress = Mathf.Clamp01(loadOp.progress / 0.9f);
                ui.SetLoadSliderValue = progress;
                if (loadOp.progress >= 0.9f)
                {
                    yield return new WaitForSeconds(1);
                    break;
                }
                yield return new WaitForSeconds(1);
            }
            ui.StartFade?.Invoke(loadOp);
        }
        #endregion

        public void CloseApps()
        {
            Application.Quit();
#if UNITY_EDITOR
            print("Quit");
#endif
        }
    }
}