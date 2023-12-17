using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class InputReader : MonoBehaviour
{
    #region Events
    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;

    public delegate void EndTouch(Vector2 position, float time); 
    public event EndTouch OnEndTouch;

    public delegate void PerformTouch(Vector2 position);
    public event PerformTouch OnPerformTouch;
    #endregion

    private TouchControlls _touchControlls;

    private void Awake()
    {
        _touchControlls = new TouchControlls();
    }

    private void OnEnable()
    {
        _touchControlls.Enable();
    }

    private void OnDisable()
    {
        _touchControlls.Disable();
    }

    private void Start()
    {
        _touchControlls.Touch.PrimaryFingerContact.started += ctx => StartTouchPrimary(ctx);
        _touchControlls.Touch.PrimaryFingerContact.canceled += ctx => EndTouchPrimary(ctx);
    }

    public Vector2 PerformTouchPimary()
    {
        return _touchControlls.Touch.PrimaryFingerPosition.ReadValue<Vector2>();
    }

    private void StartTouchPrimary(InputAction.CallbackContext ctx)
    {
        OnStartTouch?.Invoke(
            _touchControlls.Touch.PrimaryFingerPosition.ReadValue<Vector2>(), 
            (float)ctx.startTime);
    }

    private void EndTouchPrimary(InputAction.CallbackContext ctx)
    {
        OnEndTouch?.Invoke(
            _touchControlls.Touch.PrimaryFingerPosition.ReadValue<Vector2>(),
            (float)ctx.time);
    }
}