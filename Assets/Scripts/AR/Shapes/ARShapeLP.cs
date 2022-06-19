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
     => CheckSides();
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
    }

    public void PlayAnimation()
    {
        animationCoroutine = IE_StartAnimation();
        PlayCoroutine(animationCoroutine);
        animator.ResetTrigger(stopAnimTrigger);
    }

    public void StopAnimation()
    {
        animationCoroutine = IE_StopAnimation();
        PlayCoroutine(animationCoroutine);
    }
    #endregion

    private void InsertSides(int index)
    {
        sisi[index].SetActive(true);
        sisiActive[index] = sisi[index].activeSelf;
        animator.ResetTrigger(stopAnimTrigger);
    }

    private void RemoveSides(int index)
    {
        sisi[index].SetActive(false);
        sisiActive[index] = sisi[index].activeSelf;
        OnRemoveEvent?.Invoke();
        animator.ResetTrigger(stopAnimTrigger);
    }

    #region  Coroutines
    IEnumerator IE_StartAnimation()
    {
        if (jaring.Count != 0)
        {
            foreach (var item in jaring)
            {
                item.gameObject.SetActive(true);
                yield return new WaitForEndOfFrame();
            }
        }
        yield return new WaitForSeconds(0.5f);
        foreach (var text in texts)
        {
            text.SetActive(true);
            yield return new WaitForEndOfFrame();
        }
        animator.SetTrigger(startAnimTrigger);
        yield return new WaitForEndOfFrame();
    }

    IEnumerator IE_StopAnimation()
    {
        animator.SetTrigger(stopAnimTrigger);
        yield return new WaitForEndOfFrame();
        if (jaring.Count != 0)
        {
            foreach (var item in jaring)
            {
                item.gameObject.SetActive(false);
                yield return new WaitForEndOfFrame();
            }
        }
        foreach (var text in texts)
        {
            text.SetActive(false);
            yield return new WaitForEndOfFrame();
        }
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