using System.Collections.Generic;
using UnityEngine;
using ARMath.UI;

namespace ARMath.Managers
{
    public class MainMenuUIManager : MonoBehaviour
    {
        [SerializeField] internal List<UIWindow> contextWindows;
        [SerializeField] Canvas loadingCanvas;
        internal void OpenUI(UIWindow window)
        {
            foreach (var item in contextWindows)
            {
                if(item != window)
                    item.Disable();
            }
            window.Enable();
        }

        internal void CloseUI(UIWindow window)
        {
            window.Disable();
        }
    }
}