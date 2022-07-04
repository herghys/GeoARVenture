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

        private void Start()
        {
            StartCoroutine(InitFirstScene());
        }

        IEnumerator InitFirstScene()
        {
            if (MainMenuNav.Instance.ParentUIWindowIndex != -1)
            {
                OpenContextMenu(ui.ContextWindows[MainMenuNav.Instance.ParentUIWindowIndex]);
                ui.ContextWindows[MainMenuNav.Instance.ParentUIWindowIndex].mayDisable = false;
            }

            yield return new WaitForSeconds(0.3f);
			if (MainMenuNav.Instance.UIWindowIndex != -1)
				OpenContextMenu(ui.ContextWindows[MainMenuNav.Instance.UIWindowIndex]);
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

        public void RemoveLastParentWindow() => MainMenuNav.Instance.ParentUIWindowIndex = -1;

		public void SetLastParentWindow(UIWindow window) => MainMenuNav.Instance.ParentUIWindowIndex = ui.ContextWindows.IndexOf(window);

        public void RemoveLastWindow() => MainMenuNav.Instance.UIWindowIndex = -1;

		public void SetLastWindow(UIWindow window) => MainMenuNav.Instance.UIWindowIndex = ui.ContextWindows.IndexOf(window);
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