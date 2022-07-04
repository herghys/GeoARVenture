using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardFacing : MonoBehaviour
{
	[SerializeField] Camera cam;
	private void Awake()
	{
		cam = cam is null ? Camera.main.GetComponent<Camera>() : cam;
	}

	void LateUpdate()
	{
		transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward,
			cam.transform.rotation * Vector3.up);
	}
}
