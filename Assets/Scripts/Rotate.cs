using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.Events;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public UnityEvent OnRotate;

    [Header("Rotation")]
    [SerializeField] private AnimationCurve rotationCurve;
    [SerializeField] private AnimationCurve fallCurve;
    [Range(60, 90)]
    [SerializeField] private float rotationAngle = 90f;

    [Header("Parameters")]
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float jumpDuration = 0.5f;
    [SerializeField] private float fallDuration = 0.5f;

    private bool _isTapped = false;

    private void OnMouseDown() 
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (_isTapped)
            return;

        StartCoroutine(RotateObject());
    }

    /*private IEnumerator RotateObject()
    {
        _isTapped = true; 

        float rotationDuration = 0.5f;

        Vector3 startRotation = transform.eulerAngles;
        Vector3 targetRotation = startRotation + new Vector3(0, rotationAngle, 0);

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = transform.position + new Vector3(0, 1, 0);

        float elapsedTime = 0f;

        rb.AddForce(Vector3.up * 2.5f, ForceMode.Impulse);

        while (elapsedTime < rotationDuration)
        {
            float t = elapsedTime / rotationDuration;
            float curveValue = rotationCurve.Evaluate(t);
            transform.eulerAngles = Vector3.Lerp(startRotation, targetRotation, curveValue);
            transform.position = Vector3.Lerp(startPosition, targetPosition, curveValue);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }

        transform.eulerAngles = targetRotation;
        transform.position = startPosition;

        OnRotate?.Invoke();

        EventsHolder.CellTapped();

        VibrationHandler.Vibrate(50); 

        _isTapped = false;
    }*/

    private IEnumerator RotateObject()
    {
        _isTapped = true;

        Vector3 startRotation = transform.eulerAngles;
        Vector3 targetRotation = startRotation + new Vector3(0, rotationAngle, 0);

        Vector3 startPosition = transform.position;
        Vector3 jumpTarget = startPosition + new Vector3(0, jumpHeight, 0);
        Vector3 fallTarget = startPosition;

        float elapsedTime = 0f;

        while (elapsedTime < jumpDuration)
        {
            float t = elapsedTime / jumpDuration;
            float curveValue = rotationCurve.Evaluate(t);

            transform.eulerAngles = Vector3.Lerp(startRotation, targetRotation, curveValue);
            transform.position = Vector3.Lerp(startPosition, jumpTarget, curveValue);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.eulerAngles = targetRotation;
        transform.position = jumpTarget;

        elapsedTime = 0f;

        while (elapsedTime < fallDuration)
        {
            float t = elapsedTime / fallDuration;
            float curveValue = fallCurve.Evaluate(t);

            transform.position = Vector3.Lerp(jumpTarget, fallTarget, curveValue);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = fallTarget;

        OnRotate?.Invoke();

        EventsHolder.CellTapped();

        VibrationHandler.Vibrate(50);

        _isTapped = false;
    }
}