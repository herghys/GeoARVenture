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
            gameObject.SetActive(true);

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
			if (!gameObject.activeSelf) return;
			StartCoroutine(MoveWindow(direction, duration, active));
        }
        #endregion

        private IEnumerator MoveWindow(Vector3 orientation, float _duration, bool active)
        {
            tweener = transform.DOLocalMove(orientation, _duration);
            yield return tweener.SetEase(Ease.InOutSine).AsyncWaitForCompletion();

            while (tweener.IsPlaying())
            {
                yield return null;
            }

            gameObject.SetActive(active);
			isActive = active;
			//yield return tweener.AsyncWaitForCompletion();
			//gameObject.SetActive(active);
		}
    }
}