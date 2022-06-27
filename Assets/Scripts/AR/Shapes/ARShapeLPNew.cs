using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ARShapeLPNew : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] string open = "Open";
    [SerializeField] string close= "Close";
    [SerializeField] bool isOpen;
    public bool IsOpen { get => isOpen; set => isOpen = value; }
    public Animator animatorRef { get => animator; }
    private void Awake()
    {
        if (animator is null) animator.GetComponent<Animator>();
    }

    public void StartOpenAnimation()
    {
        Debug.Log("Open");
        animator.SetTrigger(open);
        if (gameObject.activeSelf)
        {
            StartCoroutine(AnimationCondition(open, close));
            isOpen = true;
        }
    }
    public void StartCloseAnimation()
    {
        Debug.Log("Close");
        animator.SetTrigger(close);
        if (gameObject.activeSelf)
        {
            StartCoroutine(AnimationCondition(close, open));
            isOpen = false;
        }
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
