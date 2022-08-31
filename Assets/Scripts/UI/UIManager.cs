using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ARMath.UI;

namespace ARMath.Managers
{
    public class UIManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] protected SceneFader fader;
        protected static UIManager instance;
        public RectTransform mainCanvas;

        [Header("Loader")]
        public Canvas loadingCanvas;
        [SerializeField] Slider loadSlider;

        [Header("UI Windows")]
        [SerializeField] internal List<UIWindow> contextWindows;
        public List<UIWindow> ContextWindows { get => contextWindows; }

        public delegate void Fade(AsyncOperation op);
        public Fade StartFade;

        #region Get Setter
        public float SetLoadSliderValue { set => loadSlider.value = value; }
        public static UIManager SharedInstance { get { return instance; } }
        #endregion

        private void Awake()
        {
            instance = this;
        }
        internal void OpenUI(UIWindow window)
        {
            window.gameObject.SetActive(true);
            window.Enable();
            foreach (var item in contextWindows)
            {
                if (item != window)
                    item.Disable();
            }
        }

        internal void CloseUI(UIWindow window)
        {
            window.Disable();
        }

        public void StartLoadLevel()
        {
            loadingCanvas.enabled = true;
        }
    }
}