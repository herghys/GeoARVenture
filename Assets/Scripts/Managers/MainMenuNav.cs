using System.Collections;
using System.Collections.Generic;
using ARMath.Managers;
using ARMath.UI;
using UnityEngine;

public class MainMenuNav : MonoBehaviour
{

	[SerializeField] int parentUWindowIndex = -1;
	public int ParentUIWindowIndex { get => parentUWindowIndex; set => parentUWindowIndex = value; }
	[SerializeField] int uIWindowIndex = -1;
	public int UIWindowIndex { get => uIWindowIndex; set => uIWindowIndex = value; }

    public static MainMenuNav Instance;
	public void Awake()
	{
		if (Instance != null) { Destroy(gameObject); } else { Instance = this; DontDestroyOnLoad(gameObject); }
	}

	public void SetWindowIndex(int index)
	{
		UIWindowIndex = index;
	}
}
