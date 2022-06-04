using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontFacingText : MonoBehaviour
{
    [SerializeField] Camera cam;

    private void Awake()
    {
        cam = cam is null ? Camera.main.GetComponent<Camera>() : cam;
    }

    private void LateUpdate()
    {
        transform.LookAt(new Vector3(cam.transform.position.x, 0, cam.transform.position.z));
    }
}
