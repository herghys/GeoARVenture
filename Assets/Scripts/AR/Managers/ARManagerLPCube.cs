using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARManagerLPCube : MonoBehaviour
{
    public CuboidShape shape = CuboidShape.Cube;
	public string cuboidText;

    public List<ARManagerLPCubeHelper> helper;

	public void ChangeShapeToCube()
    {
        shape = CuboidShape.Cube;
		cuboidText = "s x s";
        StartCoroutine(ChangeShape());
	}

    public void ChangeShapeToBalok()
    {
        shape = CuboidShape.Balok;
		cuboidText = "p x l";
		StartCoroutine(ChangeShape());
	}

    public IEnumerator ChangeShape()
    {
        foreach (var item in helper)
        {
            yield return null;
            if (item != null || item.gameObject.activeSelf)
            item.ChangeShape(shape);
        }
        yield return null;
    }
}

public enum CuboidShape
{
    Cube, Balok
}