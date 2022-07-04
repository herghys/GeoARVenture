using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QO = QuickOutline;

public class ARShapeUnsur : MonoBehaviour
{   
    [SerializeField] ARManagerUnsur manager;
    [SerializeField] Material changeMaterial;
    [SerializeField] Color highlightColor = new Color(255,121,210, 255);
    [SerializeField] QO.Outline outline;
    [SerializeField] List<QO.Outline> outlines;
    [SerializeField] MeshRenderer meshRender;

    private void Awake()
    {
        if (outline == null) outline = GetComponent<QO.Outline>();
        if (manager is null) manager = FindObjectOfType<ARManagerUnsur>();
    }

    public void ChangeColor(MeshRenderer renderer)
    {   
        if (manager.IsAnimating) return;
        if (gameObject.activeSelf)
            StartCoroutine(IE_ChangeColor(renderer, changeMaterial));
    }

    public void ChangeColor(){
        if(manager.IsAnimating)return;
        if (gameObject.activeSelf)
            StartCoroutine(IE_ChangeColor(meshRender, changeMaterial));

    }
    public void Highlight()
    {
        if (manager.IsAnimating) return;
        if (gameObject.activeSelf)
            StartCoroutine(IE_Highlight());
    }

    public void Highlight(QO.Outline _outline)
    {
        if (manager.IsAnimating) return;
        if (!gameObject.activeSelf)
            return;

        foreach (var item in outlines)
        {
            if (item == _outline)
                StartCoroutine(IE_Highlight(_outline));
        }
    }

    IEnumerator IE_ChangeColor(MeshRenderer renderer, Material mat)
    {
        var startMat = renderer.material ;

        renderer.material = changeMaterial;
		//renderer.material.color = highlightColor;
		yield return new WaitForSeconds(1.5f);

        renderer.material = startMat;
		//renderer.material.color = startColor;
		yield return null;
	}

    IEnumerator IE_ChangeColor(MeshRenderer renderer)
    {   

        var startColor = renderer.material.color;

        foreach (var item in renderer.materials)
        {
            yield return null;
            item.color = highlightColor;
        }
        //renderer.material.color = highlightColor;
        yield return new WaitForSeconds(1.5f);

        foreach (var item in renderer.materials)
        {
            yield return null;
            item.color = startColor;
        }
        //renderer.material.color = startColor;
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
        manager.IsAnimating = true;
        outline.enabled = true;
        yield return new WaitForSeconds(1.5f);
        outline.enabled = false;
        yield return new WaitForSeconds(0.1f);
        manager.IsAnimating = false;
    }

    [ContextMenu("Get Reference")]
    public void GetReference()
    {
        
        if (manager == null) manager = FindObjectOfType<ARManagerUnsur>();
    }

    [ContextMenu("Get This Reference")]
    public void GetLocalReference()
    {
        if (outline == null) outline = GetComponent<QO.Outline>();
        if (meshRender == null) meshRender = GetComponent<MeshRenderer>();
    }
}
