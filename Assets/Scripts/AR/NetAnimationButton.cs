using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARMath.AR
{
    public class NetAnimationButton : ARButton
    {
        private void OnEnable()
        {
            if (string.IsNullOrEmpty(text)) text = "Jaring";

            InitNetAnimListeners();
        }
    }
}