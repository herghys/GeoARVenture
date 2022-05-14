using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

namespace ARMath.GeoShape {
    public class ShapeSideControl : MonoBehaviour
    {
        #region Variables
        MeshRenderer meshRenderer;
        [SerializeField] int sideIndex;
        public int SideIndex { get { return sideIndex; } }

        [Header("Placement")]
        [SerializeField] bool isPlaced = false;
        [SerializeField] bool isMoving = false;
        [SerializeField] Vector3 spawnOffset;
        [SerializeField] Vector3 scaleOffset = new Vector3(1.5f, 1.5f, 1.5f);

        public bool SidePlaced { get { return isPlaced; } }
        public bool IsMoving { get {return isMoving; } }

        [Header("Rotation")]
        [SerializeField] bool isRotated = false;
        [SerializeField] bool isRotating = false;
        [SerializeField] Quaternion rotation;
        public bool SideRotated { get { return isRotated; } }
        public bool SideRotating { get { return isRotating; } }
        #endregion

        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.enabled = false;
            transform.localPosition = spawnOffset;
            transform.localScale = scaleOffset;
        }

        private void OnEnable()
        {
            SubscribeToController();
        }

        internal void Animate(int index, SideControlType control)
        {
            if (sideIndex != index) return;
            switch (control)
            {
                case SideControlType.Placement:
                    PlaceSide();
                    break;
                case SideControlType.Animation:
                    AnimateSide();
                    break;
            }
        }

        #region Side Placement
        private void PlaceSide()
        {
            if (isMoving) return;

            if (isPlaced)
                PlaceSide(targetPos:spawnOffset, scaleOffset, false) ;
            else
                PlaceSide(targetPos: Vector3.zero, scale: Vector3.one, true);
        }

        private async void PlaceSide(Vector3 targetPos, Vector3 scale, bool isPlacing, float duration = 1.25f)
        {
            if (isPlacing)
                meshRenderer.enabled = true;
            
            TweenScale(scale, duration + 0.25f);
            await TweenPosition(targetPos, duration);

            isPlaced = isPlacing;
            if (!isPlacing) meshRenderer.enabled = false;
        }
        #endregion

        #region Rotation
        private void AnimateSide()
        {

        }
        #endregion

        #region Animation Tween
        private async void TweenScale(Vector3 scale, float duration)
        {
            var tweener = transform.DOScale(scale, duration);
            tweener.SetEase(Ease.InOutSine);
            await tweener.AsyncWaitForCompletion();
        }

        private async Task TweenPosition(Vector3 targetPos, float duration)
        {
            while (transform.localPosition != targetPos)
            {
                var tweener = transform.DOLocalMove(targetPos, duration);
                tweener.SetEase(Ease.InOutSine);
                isMoving = true;
                await tweener.AsyncWaitForCompletion();
            }
            isMoving = false;
        }

        private async void TweenRotation(Quaternion rotation, float duration)
        {
            while (transform.localRotation != rotation)
            {
                var tweener = transform.DORotateQuaternion(rotation, duration);
                tweener.SetEase(Ease.InOutSine);
                await tweener.AsyncWaitForCompletion();
            }
        }
        #endregion

        #region Controller Subscribe
        void SubscribeToController()
        {
            var controller = gameObject.GetComponentInParent<ShapeController>();
            controller.Subscribe(this);
        }
        #endregion
    }
}