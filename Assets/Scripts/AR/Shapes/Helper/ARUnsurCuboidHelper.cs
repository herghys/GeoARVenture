using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuboidTransformHelper : MonoBehaviour
{
    [SerializeField] Transform reference;
    void OnEnable()
    {
        gameObject.transform.localScale = reference.localScale;
        gameObject.transform.localRotation  = reference.localRotation;
    }
}
