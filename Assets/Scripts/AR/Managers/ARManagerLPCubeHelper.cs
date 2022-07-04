using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ARMath.AR.Manager;

public class ARManagerLPCubeHelper : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] ARManagerLPCube cubeManager;
    [SerializeField] GameObject cubeImg;
    [SerializeField] GameObject balokImg;

    private void Awake()
    {
        if (text is null)
            text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ChangeShape(CuboidShape shape)
    {
        if (shape == CuboidShape.Cube)
        {
			text.text = "s x s";
			cubeImg.SetActive(true);
			balokImg.SetActive(false);
		}
        else
        {
			text.text = "p x l";
			cubeImg.SetActive(false);
			balokImg.SetActive(true);
		}

    }

    private void OnEnable()
    {
        if (cubeManager.shape == CuboidShape.Cube)
        {
            text.text = "s x s";
            cubeImg.SetActive(true);
            balokImg.SetActive(false);
        }
        else
        {
			text.text = "p x l";
			cubeImg.SetActive(false);
			balokImg.SetActive(true);
		}
    }
}
