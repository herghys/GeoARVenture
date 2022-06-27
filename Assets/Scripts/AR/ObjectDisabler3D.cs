using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ObjectDisabler3D : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent OnDisableEvent;
    private float clickDeltaTime = 0.3F;
    private float downTime = 0f;
    public bool mayRemove = true;
    bool pressed;


    private void FixedUpdate()
    {
        if (pressed)
            downTime += Time.deltaTime;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (downTime > -0.1f && downTime < 0.3f)
        {
            Debug.Log($"{this}.Clicked");
            gameObject.SetActive(false);
            OnDisableEvent?.Invoke();
        }
        pressed = false;
        downTime = 0;
    }
}
