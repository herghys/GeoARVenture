using System.Collections.Generic;
using UnityEngine;
using ARMath.Events;
using System;

namespace ARMath.GeoShape
{
    public class ShapeManager : MonoBehaviour
    {
        private static ShapeManager instance;
        //public static ShapeController Instance { get => instance ?? (instance = new ShapeController()); }
        public static ShapeManager Instance { get => instance; }


        [SerializeField] ARShapeEvents shapeEvents;

        private void Awake()
        {
            if(instance == null) instance = this;
        }

        public void OnNetClicked(int index)
            => shapeEvents.AnimateNetEventHandler?.Invoke(index);
        

        public void OnPlaceClicked(int index)
            => shapeEvents.SidePlaceEventHandler?.Invoke(index);


        public void OnAnimationPerSideClicked(int index)
            => shapeEvents.AnimateSideEventHandler?.Invoke(index);

        public void OnPlayAnimationClicked()
            => shapeEvents.PlayAnimationEventHandler?.Invoke();
    }
}