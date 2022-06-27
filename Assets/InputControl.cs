/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputControl : MonoBehaviour
{
    private MainInput input;

    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;

    public delegate void EndTouch(Vector2 position, float time);
    public event StartTouch OnEndTouch;


    private void Awake()
    {
        input = new MainInput();
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void Start()
    {
        input.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        input.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);

        input.Touch.SecondaryContact.started += _ => ZoomStart();
        input.Touch.SecondaryContact.canceled += _ => ZoomEnd();
    }

    private int StartTouchPrimary(InputAction.CallbackContext ctx)
    {
        if (OnStartTouch != null) OnStartTouch();
    }

    private void EndTouchPrimary(InputAction.CallbackContext ctx)
    {
        throw new NotImplementedException();
    }

    public void ZoomStart()
    {

    }

    public void ZoomEnd()
    {

    }
}
*/