using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARMath.UI;

namespace ARMath.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] internal List<UIWindow> contextWindows;
        protected static UIManager instance;
        public static UIManager SharedInstance { get { return instance; } }
        public RectTransform mainCanvas;
        public Canvas loadingCanvas;

        private void Awake()
        {
            instance = this;
        }
        internal void OpenUI(UIWindow window)
        {
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
    }
}