using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ARShapeLPNew : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] string open = "Open";
    [SerializeField] string close= "Close";

    private void Awake()
    {
        if (animator is null) animator.GetComponent<Animator>();
    }

    public void StartOpenAnimation()
    {
        Debug.Log("Open");
        animator.SetTrigger(open);
        StartCoroutine(AnimationCondition(open, close));
    }
    public void StartCloseAnimation()
    {
        Debug.Log("Close");
        animator.SetTrigger(close);
        StartCoroutine(AnimationCondition(close, open));
    }

    IEnumerator AnimationCondition(string toTrigger, string toReset)
    {
        animator.ResetTrigger(toReset);   
        while (animator.IsInTransition(0))
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);
        animator.ResetTrigger(toTrigger);
    }
}
