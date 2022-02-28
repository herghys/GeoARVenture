using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ARMath.UI {
    public class ButtonWindow : MonoBehaviour
    {
        [SerializeField] UnityEvent events;

        public void StartEvent()
        {
            events?.Invoke();
        }

        public void SetWindowDisable(UIWindow window)
        {
            window.mayDisable = false;
        }

        public void SetWindowEnable(UIWindow window)
        {
            window.mayDisable = true;
        }
    }
}