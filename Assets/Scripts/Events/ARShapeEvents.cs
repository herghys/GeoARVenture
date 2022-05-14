using UnityEngine;

namespace ARMath.Events
{
    [CreateAssetMenu(fileName = "New ARShape Events", menuName = "ARMath/Shape Events")]
    public class ARShapeEvents : ScriptableObject
    {
        public delegate void SidePlaceDelegate(int index);
        public SidePlaceDelegate SidePlaceEventHandler;

        public delegate void AnimateNetDelegate(int index);
        public AnimateNetDelegate AnimateNetEventHandler;

        public delegate void AnimateSideDelegate(int index);
        public AnimateSideDelegate AnimateSideEventHandler;

        public delegate void PlayAnimation();
        public PlayAnimation PlayAnimationEventHandler;
    }
}