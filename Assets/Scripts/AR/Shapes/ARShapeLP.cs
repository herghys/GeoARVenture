using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ARShapeLP : MonoBehaviour
{
    public ARShapeLuasStruct structure;

    IEnumerator animationCoroutine;
    string startAnimTrigger;
    string stopAnimTrigger;
    #region  Unity Default
    private void Awake()
    {
        startAnimTrigger = ShapeHelper.StartAnimTrigger;
        stopAnimTrigger = ShapeHelper.StopAnimTrigger;

        //animator = animator is null ? GetComponent<Animator>() : animator;
        structure.animator?.GetComponent<Animator>();
    }

    private void Start()
    {
        CheckSides();
        /*structure.animator.ResetTrigger(startAnimTrigger);
        structure.animator.ResetTrigger(stopAnimTrigger);*/
    }
    #endregion
    #region Public
    public void ControlSide(int index)
    {
        if (structure.sisi[index].activeSelf) RemoveSides(index);
        else InsertSides(index);

        CheckSides();
    }

    public void CheckSides()
    {
        if (!structure.sisiActive.All(val => val == true))
        {
            structure.animator.ResetTrigger(stopAnimTrigger);
            structure.playAnimUI.SetActive(false);
            return;
        }

        if (structure.sisiActive.Any(val => val == false))
        {
            structure.animator.ResetTrigger(stopAnimTrigger);
            structure.playAnimUI.SetActive(false);
            animationCoroutine = IE_StopAnimation();
        }
        else 
        {
            structure.playAnimUI.SetActive(true);
            animationCoroutine = null;
        }

        PlayCoroutine(animationCoroutine);
    }

    public void PlayAnimation()
    {
        structure.animator.ResetTrigger(stopAnimTrigger);
        animationCoroutine = IE_StartAnimation();
        PlayCoroutine(animationCoroutine);
    }

    public void StopAnimation()
    {
        structure.animator.ResetTrigger(startAnimTrigger);
        animationCoroutine = IE_StopAnimation();
        PlayCoroutine(animationCoroutine);
    }
    #endregion

    private void InsertSides(int index)
    {
        structure.sisi[index].SetActive(true);
        structure.sisiActive[index] = structure.sisi[index].activeSelf;
        //CheckSides();
        structure.OnRemoveEvent?.Invoke();
    }

    private void RemoveSides(int index)
    {
        structure.sisi[index].SetActive(false);
        structure.sisiActive[index] = structure.sisi[index].activeSelf;
        //CheckSides();
        structure.OnRemoveEvent?.Invoke();
    }

    #region  Coroutines
    IEnumerator IE_StartAnimation()
    {
        structure.animator.ResetTrigger(stopAnimTrigger);
        for (int i = 0; i < structure.texts.Count; i++)
        {
            yield return new WaitForSeconds(0.25f);
            structure.texts[i].SetActive(true);
        }
        yield return new WaitForEndOfFrame();

       
        structure.animator.SetTrigger(startAnimTrigger);
        while (!structure.animator.IsInTransition(0))
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
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
        yield return new WaitForEndOfFrame();

        foreach (var text in structure.texts)
        {
            yield return new WaitForSeconds(0.25f);
            text.SetActive(false);
        }
        yield return new WaitForSeconds(0.5f);
        structure.animator.ResetTrigger(stopAnimTrigger);
        yield return null;
    }
    #endregion

    #region Helper
    public void PlayCoroutine(IEnumerator coroutine)
    {
        if (coroutine != null)
            StartCoroutine(coroutine);
    }
    #endregion
}