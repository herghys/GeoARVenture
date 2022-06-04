using UnityEngine;

namespace ARMath.AR
{
    public class TargetHandler : DefaultObserverEventHandler
    {
        [SerializeField] GameObject group;
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
            CanvasGroupEnabler(true);
        }

        public void TargetLost()
        {
            print($"{shapes} lost");
            CanvasGroupEnabler(false);
        }

        public void CanvasGroupEnabler(bool _enabled)
        {
            group.SetActive(_enabled);
            /*if (group is not null)
            {
                group.alpha = alpha;
                group.blocksRaycasts = enabled;
                group.interactable = enabled;
            }*/
        }
    }
}