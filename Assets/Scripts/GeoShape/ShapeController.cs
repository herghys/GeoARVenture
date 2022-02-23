using UnityEngine;
using DG.Tweening;
using ARMath.Events;
using System;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ARMath.GeoShape
{
    public class ShapeController : MonoBehaviour
    {
        [SerializeField] bool[] isAllSidesPlaced;
        [SerializeField] List<ShapeSideControl> sideControls;
        [SerializeField] List<ShapeNetControl> netControls;
        [SerializeField] ARShapeEvents shapeEvents;

        private void OnEnable()
        {
            shapeEvents.SidePlaceEventHandler += OnSidePlace;
            shapeEvents.AnimateNetEventHandler += OnAnimateNet;
            shapeEvents.AnimateSideEventHandler += OnAnimateSide;
            shapeEvents.PlayAnimationEventHandler += OnAnimateObject;
        }

        internal void Unsubscribe(ShapeSideControl control)
        {
            sideControls.Remove(control);
        }

        internal void Subscribe(ShapeSideControl control)
        {
            if (sideControls == null) sideControls = new List<ShapeSideControl> ();
            sideControls.Add (control);
        }
        internal void Subscribe(ShapeNetControl control)
        {
            if(netControls == null) netControls = new List<ShapeNetControl> ();
            netControls.Add(control);
        }

        private void OnSidePlace(int index)
        {
            foreach (var item in sideControls)
            {
                item.Animate(index, SideControlType.Placement);
            }
        }
        private void OnAnimateSide(int index)
        {
            foreach (var item in sideControls)
            {
                item.Animate(index, SideControlType.Animation);
            }
        }

        private void OnAnimateNet(int index)
        {
            foreach (var item in netControls)
            {

            }
        }

        private void OnAnimateObject()
        {
            throw new NotImplementedException();
        }

    }

    internal enum SideControlType
    {
        Placement,
        Animation
    }
}