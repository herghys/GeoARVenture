using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectRotator : MonoBehaviour
{
    public float pcRotationSpeed = 100f;
    public float mobileRotationSpeed = 80f;

    float speed;
    [SerializeField] bool isDragging;

    public Camera cam;

    public Rigidbody rb;

#if UNITY_EDITOR
    [SerializeField] bool useAndroid;
#endif

    private void Awake()
    {
        if (rb is null) rb = gameObject.AddComponent<Rigidbody>();

        SetInitialSpeed();
        SetRigidBody();
    }

    void SetInitialSpeed()
    {
#if UNITY_EDITOR
        if (!useAndroid) speed = pcRotationSpeed;
        else speed = mobileRotationSpeed;
#elif UNITY_ANDROID
        speed = mobileRotationSpeed;
#else
        speed = pcRotationSpeed;
#endif
    }

    private void Update()
    {
        if (Input.touchSupported && Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
                isDragging = true;
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
                isDragging = false;
        }
    }

    void SetRigidBody()
    {
        rb.drag = speed * 0.75f;
        rb.angularDrag = 1;
        rb.useGravity = false;
    }

    void Drag()
    {
        if (isDragging)
        {
            float rotX = Input.GetAxis("Mouse X") * speed * Time.fixedDeltaTime;
            float rotY = Input.GetAxis("Mouse Y") * speed * Time.fixedDeltaTime;

            rb.AddTorque(Vector3.down * rotX);
            rb.AddTorque(Vector3.right * rotY);
        }
    }

#if !UNITY_ANDROID
    private void OnMouseDrag()
    {
        isDragging = true;
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }
#endif

    private void FixedUpdate()
    {
        Drag();
    }

#if UNITY_ANDROID
    private void OnValidate()
    {
        if (rb is null) rb = GetComponent<Rigidbody>();
        if (rb.useGravity) rb.useGravity= false;
    }
#endif
}

/*    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
            isDragging = false;
    }*/

/*float rotX = Input.GetAxis("Mouse X") * pcRotationSpeed * Mathf.Deg2Rad;
float rotY = Input.GetAxis("Mouse Y") * pcRotationSpeed * Mathf.Deg2Rad;

transform.Rotate(Vector3.up, -rotX);
transform.Rotate(Vector3.right, rotY);*/

/*transform.Rotate(Vector3.down, rotX);
transform.Rotate(Vector3.right, rotY);*/

/*float rotX = Input.GetAxis("Mouse X") * speed * Time.fixedDeltaTime;
float rotY = Input.GetAxis("Mouse Y") * speed * Time.fixedDeltaTime;*/