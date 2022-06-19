using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ARShapeVolume : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] List<GameObject> objectToHide;
    [SerializeField] bool isPlaying;
    private void Awake()
    {
        animator?.GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        isPlaying = !isPlaying;
    }
}
