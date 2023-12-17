using System.Collections;
using UnityEngine;

public class PinchDetection : MonoBehaviour
{
    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] private float minFieldOfView = 5f;
    [SerializeField] private float maxFieldOfView = 7.5f;
    [SerializeField] private float pinchThreshold = 10f;

    private TouchControlls _controlls;
    private Coroutine _currentRoutine;
    private Camera _camera; 

    private void Awake()
    {
        _controlls = new TouchControlls();
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        _controlls.Enable();
    }

    private void OnDisable()
    {
        _controlls.Disable();
    }

    private void Start()
    {
        _controlls.Touch.SecondaryFingerContact.started += _ => ZoomStart();
        _controlls.Touch.SecondaryFingerContact.canceled += _ => ZoomEnd();
    }

    private void ZoomStart()
    {
        _currentRoutine = StartCoroutine(ZoomDetection());
    }

    private void ZoomEnd()
    {
        StopCoroutine(_currentRoutine);
    }

    private IEnumerator ZoomDetection()
    {
        float previousDistance = Vector2.Distance(
           _controlls.Touch.PrimaryFingerPosition.ReadValue<Vector2>(),
           _controlls.Touch.SecondaryFingerPosition.ReadValue<Vector2>());

        while (true)
        {
            float currentDistance = Vector2.Distance(
           _controlls.Touch.PrimaryFingerPosition.ReadValue<Vector2>(),
           _controlls.Touch.SecondaryFingerPosition.ReadValue<Vector2>());

            float deltaDistance = currentDistance - previousDistance;

            float targetFieldOfView = Mathf.Clamp(_camera.fieldOfView - deltaDistance * zoomSpeed, minFieldOfView, maxFieldOfView);
            _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, targetFieldOfView, Time.deltaTime * zoomSpeed);
            
            previousDistance = currentDistance;
            yield return null;
        }
    }
}