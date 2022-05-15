using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using ARMath.Managers;
using System.Collections;

namespace ARMath.UI
{
    public class UIWindow : MonoBehaviour
    {
        private Tweener tweener;
        [SerializeField] bool isActive = false;
        public bool IsActive { get { return isActive; } }

        public bool mayDisable = true;
        [SerializeField] UIManager ui;

        enum MoveOrientation
        {
            X, Y
        }

        [SerializeField] MoveOrientation moveOrientation;

        #region Enable/Disable
        public void Enable()
        {
            Vector3 direction = Vector3.zero;
            ControlUI(direction, active: true);
        }

        public void Disable()
        {
            Rect targetRect = UIManager.SharedInstance.mainCanvas.rect;
            Vector3 direction = new Vector3();

            switch (moveOrientation)
            {
                case MoveOrientation.X:
                    direction = new Vector3(-targetRect.width-10, 0,0);
                    break;
                case MoveOrientation.Y:
                    direction = new Vector3(0, -targetRect.height-10,0);
                    break;
            }
            if(mayDisable)
                ControlUI(direction);
        }
        #endregion
        #region OpenClose
        void ControlUI(Vector3 direction, float duration = 0.5f, bool active = false)
        {
            StartCoroutine(MoveWindow(direction, duration));
            isActive = active;
        }
        #endregion

        private IEnumerator MoveWindow(Vector3 orientation, float _duration)
        {
            tweener = transform.DOLocalMove(orientation, _duration);
            tweener.SetEase(Ease.InOutSine);
            yield return tweener.AsyncWaitForCompletion();
        }
    }
}