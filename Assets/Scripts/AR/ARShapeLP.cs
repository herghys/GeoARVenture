using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ARShapeLP : MonoBehaviour, IDragHandler
{
    [SerializeField] Animator animator;
    [SerializeField] List<GameObject> sisi;
    [SerializeField] List<bool> sisiActive;
    [SerializeField] bool playing = false;

    [SerializeField] GameObject playAnimObject;

    private void Awake()
    {
        animator = animator is null ? GetComponent<Animator>() : animator;
    }

    private void Start()
    {
        CheckSides();
    }

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
            animator.SetTrigger("Reverse");
            playAnimObject.SetActive(false);
        }
        else
            playAnimObject.SetActive(true);
    }

    public void PlayAnimation()
    {
        playing = !playing;
        if (playing) animator.SetTrigger("Forward");
        else animator.SetTrigger("Reverse");
    }

    private void InsertSides(int index)
    {
        sisi[index].SetActive(true);
        sisiActive[index] = sisi[index].activeSelf;
    }

    private void RemoveSides(int index)
    {
        sisi[index].SetActive(false);
        sisiActive[index] = sisi[index].activeSelf;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log(eventData);
    }
}
