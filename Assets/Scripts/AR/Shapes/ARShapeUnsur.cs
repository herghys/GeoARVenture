using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QO = QuickOutline;

public class ARShapeUnsur : MonoBehaviour
{
    [SerializeField] Color highlightColor;
    [SerializeField] QO.Outline outline;
    [SerializeField] List<QO.Outline> outlines;

    private void Awake()
    {
        if (outline == null) outline = GetComponent<QO.Outline>();
    }

    public void ChangeColor(MeshRenderer renderer)
    {

        if (gameObject.activeSelf)
            StartCoroutine(IE_ChangeColor(renderer));
    }
    public void Highlight()
    {
        if (gameObject.activeSelf)
            StartCoroutine(IE_Highlight());
    }

    public void Highlight(QO.Outline _outline)
    {
        if (!gameObject.activeSelf)
            return;

        foreach (var item in outlines)
        {
            if (item == _outline)
                StartCoroutine(IE_Highlight(_outline));
        }
    }

    IEnumerator IE_ChangeColor(MeshRenderer renderer)
    {
        var startColor = renderer.material.color;

        renderer.material.color = highlightColor;
        yield return new WaitForSeconds(1.5f);
        renderer.material.color = startColor;
        yield return null;
    }
    IEnumerator IE_Highlight(QO.Outline _outline)
    {
        _outline.enabled = true;
        yield return new WaitForSeconds(1.5f);
        _outline.enabled = false;
        yield return null;
    }

    IEnumerator IE_Highlight()
    {
        outline.enabled = true;
        yield return new WaitForSeconds(1.5f);
        outline.enabled = false;
        yield return null;
    }

    [ContextMenu("Get Reference")]
    public void GetReference()
    {
        if (outline == null) outline = GetComponent<QuickOutline.Outline>();
    }
}
