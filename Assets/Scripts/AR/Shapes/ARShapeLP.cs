using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))]
public class ARShapeLP : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] bool isPlaying = false;
    [SerializeField] GameObject playAnimUI;
    public UnityEvent OnRemoveEvent;
    IEnumerator animationCoroutine;

    #region Lists
    [SerializeField] List<GameObject> sisi;
    [SerializeField] List<GameObject> jaring;
    [SerializeField] List<bool> sisiActive;
    [SerializeField] List<GameObject> texts;
    #endregion

    string startAnimTrigger = "StartAnimation";
    string stopAnimTrigger = "StopAnimation";
    #region  Unity Default
    private void Awake()
    {
        //animator = animator is null ? GetComponent<Animator>() : animator;
        animator?.GetComponent<Animator>();
    }

    private void Start()
    {
        CheckSides();
    }
    #endregion
    #region Public
    public void ControlSide(int index)
    {
        if (sisi[index].activeSelf) RemoveSides(index);
        else InsertSides(index);

        CheckSides();
    }

    public void CheckSides()
    {
        if (!sisiActive.All(val => val == true))
        {
            playAnimUI.SetActive(false);
            return;
        }

        if (sisiActive.Any(val => val == false))
        {
            playAnimUI.SetActive(false);
            animationCoroutine = IE_StopAnimation();
        }
        else 
        {
            playAnimUI.SetActive(true);
            animationCoroutine = null;
        }

        PlayCoroutine(animationCoroutine);
        /*if (sisiActive.Any(val => val == false))
        {
            playAnimUI.SetActive(false);
            animationCoroutine = IE_StopAnimation();

        }
        else
        {
            playAnimUI.SetActive(true);
            animationCoroutine = null;
        }
        PlayCoroutine(animationCoroutine);*/
    }

    public void PlayAnimation()
    {
        animator.ResetTrigger(stopAnimTrigger);
        animationCoroutine = IE_StartAnimation();
        PlayCoroutine(animationCoroutine);
    }

    public void StopAnimation()
    {
        animator.ResetTrigger(startAnimTrigger);
        animationCoroutine = IE_StopAnimation();
        PlayCoroutine(animationCoroutine);
    }
    #endregion

    private void InsertSides(int index)
    {
        sisi[index].SetActive(true);
        sisiActive[index] = sisi[index].activeSelf;
    }

    private void RemoveSides(int index)
    {
        sisi[index].SetActive(false);
        sisiActive[index] = sisi[index].activeSelf;
        OnRemoveEvent?.Invoke();
    }

    #region  Coroutines
    IEnumerator IE_StartAnimation()
    {
        for (int i = 0; i < texts.Count; i++)
        {
            yield return new WaitForSeconds(0.25f);
            texts[i].SetActive(true);
        }
        yield return new WaitForEndOfFrame();
        if (jaring.Count != 0)
        {
            foreach (var item in jaring)
            {
                yield return new WaitForSeconds(0.25f);
                item.gameObject.SetActive(true);
            }
        }
        yield return new WaitForEndOfFrame();

        animator.ResetTrigger(stopAnimTrigger);
        animator.SetTrigger(startAnimTrigger);
        while (!animator.IsInTransition(0))
        {
            yield return null;
        }
        animator.ResetTrigger(startAnimTrigger);
        yield return null;
    }


    IEnumerator IE_StopAnimation()
    {
        animator.SetTrigger(stopAnimTrigger);
        while (!animator.IsInTransition(0))
        {
            yield return null;
        }
        yield return new WaitForEndOfFrame();
        if (jaring.Count != 0)
        {
            foreach (var item in jaring)
            {
                yield return new WaitForSeconds(0.25f);
                item.gameObject.SetActive(false);
            }
        }
        foreach (var text in texts)
        {
            yield return new WaitForSeconds(0.25f);
            text.SetActive(false);
        }
        animator.ResetTrigger(stopAnimTrigger);
        yield return null;
    }
    #endregion

    #region Helper
    private void PlayCoroutine(IEnumerator coroutine)
    {
        if (coroutine != null)
            StartCoroutine(coroutine);
    }
    #endregion
}