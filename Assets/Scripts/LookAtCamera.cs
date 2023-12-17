using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform _target;

    private void Awake() => _target = Camera.main.transform;

    private void LateUpdate()
    {
        transform.LookAt(_target);
        transform.forward = -transform.forward;
    }
}
