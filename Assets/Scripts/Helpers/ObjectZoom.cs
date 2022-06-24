using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Transform))]
public class ObjectZoom : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 0.05f;
    [SerializeField] float touchSpeed = 0.05f;

    [SerializeField] float scale;
    [SerializeField] float initialScale = 1f;
    [SerializeField] float minScale = 0.01f;
    [SerializeField] float maxScale = 50f;

    [SerializeField] TextMeshProUGUI textDebug;


    private void Awake()
    {
        scale = initialScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchSupported)
        {
            if (Input.touchCount == 2)
            {

                // get current touch positions
                Touch tZero = Input.GetTouch(0);
                Touch tOne = Input.GetTouch(1);
                // get touch position from the previous frame
                Vector2 tZeroPrevious = tZero.position - tZero.deltaPosition;
                Vector2 tOnePrevious = tOne.position - tOne.deltaPosition;

                float oldTouchDistance = Vector2.Distance(tZeroPrevious, tOnePrevious);
                float currentTouchDistance = Vector2.Distance(tZero.position, tOne.position);

                // get offset value
                float deltaDistance = oldTouchDistance - currentTouchDistance;
                Debug.Log(deltaDistance);

                textDebug.text = deltaDistance.ToString();
                TouchZoom(deltaDistance);
                //Zoom(deltaDistance, TouchZoomSpeed);
            }
        }
        {
            float scroll = Input.mouseScrollDelta.y;
            Debug.Log(scroll);

            if (scroll < -0.5f)
                ScrollZoom(false);
            else if (scroll > 0.5f)
                ScrollZoom();
        }
    }

    void TouchZoom(float deltaMagnitudeDiff)
    {
        if (transform.localScale.x <= minScale || transform.localScale.x >= maxScale)
            return;

        scale += deltaMagnitudeDiff * -scrollSpeed;
        transform.localScale = Vector3.one * scale;
    }

    void ScrollZoom(bool zoomIn = true)
    {
        var transformScale = transform.localScale;
        if (transformScale.x <= minScale || transformScale.x >= maxScale)
            return;

        if (zoomIn)
            scale += scrollSpeed;
        else
            scale -= scrollSpeed;

        transform.localScale = (Vector3.one * scale);
    }

}