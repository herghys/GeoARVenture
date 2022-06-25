using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ObjectZoom : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 0.05f;
    [SerializeField] ObjectRotator rotator;

    [SerializeField] float scale;
    [SerializeField] float initialScale = 1f;
    [SerializeField] float minScale = 0.2f;
    [SerializeField] float maxScale = 10f;

    private void Awake()
    {
        scale = initialScale;
        if (rotator is null) rotator = GetComponent<ObjectRotator>();
    }

    void FixedUpdate()
    {
        if (Input.touchSupported)
        {
            if (Input.touchCount == 2)
            {
                Touch tZero = Input.GetTouch(0);
                Touch tOne = Input.GetTouch(1);
                
                Vector2 tZeroPrevious = tZero.position - tZero.deltaPosition;
                Vector2 tOnePrevious = tOne.position - tOne.deltaPosition;

                float oldTouchDistance = Vector2.Distance(tZeroPrevious, tOnePrevious);
                float currentTouchDistance = Vector2.Distance(tZero.position, tOne.position);

                float deltaDistance = oldTouchDistance - currentTouchDistance;
                
                if (deltaDistance > 0.1f)
                    Zoom(false);
                if (deltaDistance < -0.2f)
                    Zoom(true);
            }
        }
        {
            float scroll = Input.mouseScrollDelta.y;

            if (scroll < -0.5f)
                Zoom(false);
            else if (scroll > 0.5f)
                Zoom();
        }
    }

    void Zoom(bool zoomIn = true)
    {
        if (scale < minScale)
        {
            scale = minScale + 0.015f;
            rotator.mobileRotationSpeed = 5f;
            return;
        }

        if (scale > maxScale)
        {
            scale = maxScale - 0.015f;
            return;
        }

        if (zoomIn)
        {
            scale += scrollSpeed;
            rotator.mobileRotationSpeed = rotator.mobileRotationSpeed += 7.5f;
        }
        else
        {
            scale -= scrollSpeed;
            rotator.mobileRotationSpeed = rotator.mobileRotationSpeed -= 7.5f;
        }

        transform.localScale = (Vector3.one * scale);
        //transform.localScale = Vector3.one * speed; 
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        scale = initialScale;
        if (rotator is null) rotator = GetComponent<ObjectRotator>();
    }
#endif
}