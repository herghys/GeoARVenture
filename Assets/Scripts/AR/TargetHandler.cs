using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

namespace ARMath.AR
{
    public class TargetHandler : DefaultObserverEventHandler
    {
        [SerializeField] CanvasGroup group;
        [SerializeField] GeometryShapes shapes;

        protected override void OnTrackingFound()
        {
            base.OnTrackingFound();
            TargetFound();
        }

        protected override void OnTrackingLost()
        {
            base.OnTrackingLost();
            TargetLost();
        }

        public void TargetFound()
        {
            print($"{shapes} found");
            CanvasGroupEnabler(1, true);
        }

        public void TargetLost()
        {
            print($"{shapes} lost");
            CanvasGroupEnabler(0, false);
        }

        public void CanvasGroupEnabler(float alpha, bool _enabled)
        {
            group.alpha = alpha;
            group.blocksRaycasts = enabled;
            group.interactable = enabled;
        }
    }
}