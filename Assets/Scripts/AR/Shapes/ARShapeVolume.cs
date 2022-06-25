using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ARShapeVolume : MonoBehaviour
{
    public ARShapeVolumeStruct structure;

    [SerializeField] bool isPlaying;

    #nullable enable
    IEnumerator? animationCoroutine;
    string startAnimTrigger;
    string stopAnimTrigger;

    private void Awake()
    {
        startAnimTrigger = ShapeHelper.StartAnimTrigger;
        stopAnimTrigger = ShapeHelper.StopAnimTrigger;

        if (structure.animator == null) structure.animator = GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        structure.animator.ResetTrigger(stopAnimTrigger);
        animationCoroutine = IE_StartAnimation();
        PlayCoroutine(animationCoroutine);
    }

    public  void StopAnimation()
    {
        structure.animator.ResetTrigger(startAnimTrigger);
        animationCoroutine = IE_StopAnimation();
        PlayCoroutine(animationCoroutine);
    }

    IEnumerator IE_StartAnimation() 
    {
        structure.animator.ResetTrigger(stopAnimTrigger);
        if (structure.objectToHide.Count != 0)
        {
            foreach (var item in structure.objectToHide)
            {
                yield return new WaitForSeconds(0.2f);
                item.SetActive(true);
            }
        }
        structure.animator.SetTrigger(startAnimTrigger);
        yield return new WaitForSeconds(0.2f);
        while (!structure.animator.IsInTransition(0))
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);
        structure.animator.ResetTrigger(startAnimTrigger);
        yield return null; 
    }
    IEnumerator IE_StopAnimation() 
    {
        structure.animator.ResetTrigger(startAnimTrigger);
        structure.animator.SetTrigger(stopAnimTrigger);
        while (!structure.animator.IsInTransition(0))
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);
        if (structure.objectToHide.Count != 0)
        {
            foreach (var item in structure.objectToHide)
            {
                yield return new WaitForSeconds(0.2f);
                item.SetActive(false);
            }
        }
        structure.animator.SetTrigger(stopAnimTrigger);
        yield return null; 
    }

    public void PlayCoroutine(IEnumerator coroutine) 
    {
        if (coroutine != null)
            StartCoroutine(coroutine);
    }
}


/*[SerializeField] Animator animator;
    [SerializeField] List<GameObject> objectToHide;
    [SerializeField] bool isPlaying;
    private void Awake()
    {
        animator?.GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        isPlaying = !isPlaying;
    }*/