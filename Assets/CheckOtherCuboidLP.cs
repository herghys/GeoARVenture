using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOtherCuboidLP : MonoBehaviour
{
    [SerializeField] ObjectDisabler3D thisDisabler;
    [SerializeField] ObjectDisabler3D neighborDisabler;

    private void OnEnable()
    {
        if (neighborDisabler.isDisabled)
            thisDisabler.DisableObject();
        else
            thisDisabler.EnableObject();
    }

    private void Awake()
    {
		if (neighborDisabler.isDisabled)
			thisDisabler.DisableObject();
		else
			thisDisabler.EnableObject();
	}

#if UNITY_EDITOR
    private void OnValidate()
    {
        thisDisabler = GetComponent<ObjectDisabler3D>();
    }
#endif
}
