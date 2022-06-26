using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class ShapeHelper
{
    public const string StartAnimTrigger = "StartAnimation";
    public const string StopAnimTrigger = "StopAnimation";
}

[System.Serializable]
public struct ARShapeUnsurStruct
{
    public Shape shapeType;
    public MateriType materiType;

    public Color changeColor;
    public List<Color> baseColor;
    public List<Material> baseMaterials;

}

[System.Serializable]
public struct ARShapeVolumeStruct
{
    public Shape shapeType;
    public MateriType materiType;

    public Animator animator;
    public bool isPlaying;

    #region Volume
    public List<GameObject> objectToHide;
    #endregion
    public UnityEvent OnAddEvent;
    public UnityEvent OnRemoveEvent;
}

[System.Serializable]
public struct ARShapeLuasStruct
{
    public Shape shapeType;
    public MateriType materiType;
    public GameObject playAnimUI;

    public Animator animator;

   /* #region Luas Permukaan
    public List<GameObject> sisi;
    public List<bool> sisiActive;
    public List<GameObject> texts;
    #endregion*/

    public UnityEvent OnAddEvent;
    public UnityEvent OnRemoveEvent;
}

public enum Shape
{
    Cuboid, Prism, Pyramid
}

public enum MateriType
{
    Unsur, Luas, Volume
}