using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ObjectDisabler3D : MonoBehaviour
{
    public UnityEvent OnDisableEvent;
    public ObjectRotator rotator;

    private void OnMouseUp()
    {
        if (rotator.mayDrag)
        {
            Debug.Log($"{this}.Clicked");
            gameObject.SetActive(false);
            OnDisableEvent?.Invoke();
        }
    }
}
