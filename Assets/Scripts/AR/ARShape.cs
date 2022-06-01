using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARShape : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] List<GameObject> sisi;


    private void Awake()
    {
        animator = animator is null ? GetComponent<Animator>() : animator;
    }

    public void InsertSides(int sideIndex)
    {
        sisi[sideIndex].SetActive(true);
    }

    public void RemoveSides(int sideIndex)
    {
        sisi[sideIndex].SetActive(false);
    }
}
