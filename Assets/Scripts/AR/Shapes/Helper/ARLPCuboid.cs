using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARLPCuboid : MonoBehaviour
{
    [SerializeField] ARShapeLPNew reference;
    [SerializeField] ARShapeLPNew thisRef;
    [SerializeField] string forceOpen = "ForceOpen";
    [SerializeField] string forceClose = "ForceClose";

    private void OnEnable()
    {
        reference.animatorRef.ResetTrigger(forceClose);
        reference.animatorRef.ResetTrigger(forceOpen);

        thisRef.IsOpen = reference.IsOpen;
        Debug.Log("ASHDBNAS");

        if (thisRef.IsOpen)
        {
            thisRef.animatorRef.SetTrigger(forceOpen);
            reference.animatorRef.ResetTrigger(forceClose);
            reference.animatorRef.ResetTrigger(forceOpen);
        }
        else
        {
            thisRef.animatorRef.SetTrigger(forceClose);
            reference.animatorRef.ResetTrigger(forceClose);
            reference.animatorRef.ResetTrigger(forceOpen);

        }
    }
}
