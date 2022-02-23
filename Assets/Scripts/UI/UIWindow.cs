using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

namespace ARMath.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIWindow : MonoBehaviour
    {
        private Tweener tweener;
        [SerializeField] bool isActive = false;
        public bool IsActive { get { return isActive; } }
        [SerializeField] private CanvasGroup canvasGroup;

        public int windowIndex;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            CloseUI();
        }

        public void Enable()
        {
            OpenUI();
        }

        public void Disable()
        {
            CloseUI();
        }
        async void OpenUI()
        {
            isActive = true;
            SetCanvasGroup(1, true);
            await TweenScaleUI(1);
        }

        async void CloseUI()
        {
            isActive = false;
            await TweenScaleUI(0.25f);
            SetCanvasGroup(0, false);
        }

        private async void SetCanvasGroup(float alpha, bool interactable)
        {
            canvasGroup.alpha = alpha;
            canvasGroup.interactable = interactable;
            canvasGroup.blocksRaycasts = interactable;
            await Task.Yield();
        }

        private async Task TweenScaleUI(float scale, float duration = 0.15f)
        {
            tweener = transform.DOScale(scale, duration);
            tweener.SetEase(Ease.InOutSine);
            await tweener.AsyncWaitForCompletion();
        }
    }
}