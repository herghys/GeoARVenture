using UnityEngine;
using UnityEngine.SceneManagement;
using ARMath.UI;

namespace ARMath.Managers
{
    public class MainMenuManager : MonoBehaviour
    {
        MainMenuUIManager ui;

        private void Awake()
        {
            ui = GetComponent<MainMenuUIManager>();
        }

        public void OpenContextMenu(UIWindow window)
        {
            ui.OpenUI(window);
        }

        public void CloseContextMenu(UIWindow window)
        {
            ui.CloseUI(window);
        }

        public void LoadScene(string scene)
        {
            SceneManager.LoadScene(scene);
        }

        public void CloseApps()
        {
            Application.Quit();
#if UNITY_EDITOR
            print("Quit");
#endif
        }
    }
}