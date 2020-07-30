using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    [SerializeField]
    protected AudioSource myFx;
    [SerializeField]
    protected AudioClip clickFx;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void ClickSound()
    {
        myFx.PlayOneShot(clickFx);
    }
}
