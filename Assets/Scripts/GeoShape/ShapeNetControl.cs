using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARMath.GeoShape
{
    public class ShapeNetControl : MonoBehaviour
    {
        [SerializeField] int netIndex;
        [SerializeField] float duration = 1.25f;
        [SerializeField] bool isRotating = false;
        [SerializeField] bool isRotated = false;
        [SerializeField] Vector3 rotationTarget;
        [SerializeField] Vector3 rotationInitial;
        #region Animate Net

        private void Awake()
        {
            SubscribeToController();
        }
        void SubscribeToController()
        {
            var controller = gameObject.GetComponentInParent<ShapeController>();
            controller.Subscribe(this);
        }

        internal void Animate(int index)
        {
            if (netIndex != index) return;

            if (isRotating) return;

            if (!isRotated) TweenRotation(rotationTarget, duration, true );
            /*if (!isRotated) TweenRotation(rotation, duration);
            else TweenRotation(rotation, duration);*/
        }

        private async void TweenRotation(Vector3 rotation, float _duration, bool rotated)
        {
            isRotating = true;
            var tweener = transform.DOLocalRotate(rotation, duration);
            tweener.SetEase(Ease.InOutSine);
            await tweener.AsyncWaitForCompletion();

            isRotating = false;
            isRotated = rotated;
        }
        #endregion
    }
}