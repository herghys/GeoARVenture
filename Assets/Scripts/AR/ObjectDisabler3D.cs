using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ObjectDisabler3D : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public MeshRenderer renderer;
    public UnityEvent OnDisableEvent;
    private float clickDeltaTime = 0.3F;
    private float downTime = 0f;
    public bool mayRemove = true;
    bool pressed;

    private void Awake()
    {
        renderer = GetComponent<MeshRenderer>();
    }

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
            renderer.enabled = false;
            //gameObject.SetActive(false);
            OnDisableEvent?.Invoke();
        }
        pressed = false;
        downTime = 0;
    }
    public void EnableObject()
    {
        renderer.enabled = true;
    }

    [ContextMenu("Get")]
    public void GetRef()
    {
        renderer = GetComponent<MeshRenderer>();
    }
}
