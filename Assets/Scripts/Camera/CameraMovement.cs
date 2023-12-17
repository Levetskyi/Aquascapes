using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float distanceX = 15f;
    [SerializeField] private float distanceZ = 15f;
    [SerializeField] private float movementTime = 0.1f;

    private Vector3 _targetPosition;
    private float _maxX, _minX;
    private float _maxZ, _minZ;

    private void Start() => Init();

    private void Update() => Movement();

    private void Init()
    {
        _targetPosition = transform.position;

        _maxX = transform.localPosition.x + distanceX;
        _minX = transform.localPosition.x - distanceX;
        _maxZ = transform.localPosition.z + distanceZ;
        _minZ = transform.localPosition.z - distanceZ;
    }

    private void Movement()
    {
        if (Input.GetMouseButton(0))
        {
            float inputX = Input.GetAxis("Mouse X") * moveSpeed;
            float inputZ = Input.GetAxis("Mouse Y") * moveSpeed;

            _targetPosition = new Vector3(
                Mathf.Clamp(transform.position.x - inputX, _minX, _maxX),
                transform.localPosition.y,
                Mathf.Clamp(transform.position.z - inputZ, _minZ, _maxZ)
            );
        }

        transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * movementTime);
    }
}