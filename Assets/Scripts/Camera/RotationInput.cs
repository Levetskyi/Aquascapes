using UnityEngine.InputSystem;
using UnityEngine;

public class RotationInput : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 250f;
    [SerializeField] private float rotationTime = 0.1f;

    private Quaternion _targetRotation;
    private float _rotationInput;

    private TouchControlls _controlls;

    private void Awake()
    {
        _controlls = new TouchControlls();
    }

    private void OnEnable()
    {
        _controlls.Touch.PrimaryFingerDelta.started += StartRotation;
        _controlls.Touch.PrimaryFingerDelta.performed += UpdateRotation;
        _controlls.Touch.PrimaryFingerDelta.canceled += EndRotation;

        _controlls.Enable();
    }

    private void OnDisable()
    {
        _controlls.Touch.PrimaryFingerDelta.started -= StartRotation;
        _controlls.Touch.PrimaryFingerDelta.performed -= UpdateRotation;
        _controlls.Touch.PrimaryFingerDelta.canceled -= EndRotation;

        _controlls.Disable();
    }

    private void StartRotation(InputAction.CallbackContext ctx)
    {
        _rotationInput = ctx.ReadValue<Vector2>().x;
    }

    private void UpdateRotation(InputAction.CallbackContext ctx)
    {
        _rotationInput = ctx.ReadValue<Vector2>().x;
    }

    private void EndRotation(InputAction.CallbackContext ctx)
    {
        _rotationInput = 0f;
    }

    public void Update()
    {
        float rotationAmount = _rotationInput * rotationSpeed * Time.deltaTime;

        _targetRotation = Quaternion.Euler(0f, rotationAmount, 0f) * transform.rotation;

        transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, rotationTime);
    }
}