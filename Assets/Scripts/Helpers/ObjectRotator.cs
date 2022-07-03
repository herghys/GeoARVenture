using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectRotator : MonoBehaviour
{
    float speed;

    [SerializeField] bool isDragging;
    [SerializeField] Vector3 intialPos;

    public float pcRotationSpeed = 100f;
    public float mobileRotationSpeed = 50f;
    public Camera cam;
    public Rigidbody rb;

    #if UNITY_EDITOR
    [SerializeField] bool useAndroid;
    #endif

    private void Awake()
    {
        if (rb is null) rb = gameObject.AddComponent<Rigidbody>();
#if UNITY_ANDROID
        mobileRotationSpeed = 40f;
#endif
        intialPos = transform.localPosition;
        SetInitialSpeed();
        SetRigidBody();
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

		if (transform.localPosition.x != intialPos.x ||
            transform.localPosition.y != intialPos.y ||
            transform.localPosition.z != intialPos.z)
			transform.localPosition = intialPos;
	}

    private void FixedUpdate()
    {
        Drag();
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

    void SetRigidBody()
    {
#if UNITY_ANDROID || UNITY_IOS
        rb.drag = speed * 0.75f * 4f;
#else
        rb.drag = speed * 0.75f;
#endif
        rb.angularDrag = 1;
        rb.useGravity = false;
    }

    void Drag()
    {
        if (isDragging)
        {

            float rotX = Input.GetAxis("Mouse X") * speed * Time.fixedDeltaTime;
            float rotY = Input.GetAxis("Mouse Y") * speed * Time.fixedDeltaTime;

            if (rotX != 0)
            rb.AddTorque(Vector3.down * rotX);
            if (rotY != 0)
            rb.AddTorque(Vector3.right * rotY);
        }
    }

#if UNITY_EDITOR
    private void OnMouseDrag()
    {
        isDragging = true;
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }
#endif

#if UNITY_ANDROID
    private void OnValidate()
    {
        if (rb is null) rb = GetComponent<Rigidbody>();
        if (rb.useGravity) rb.useGravity= false;
    }
#endif
}
