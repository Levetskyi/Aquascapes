using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float distanceX = 15f;
    [SerializeField] private float distanceZ = 15f;
    [SerializeField] private float movementTime = 0.1f;

    private Camera _cachedCamera;

    private Vector3 _targetPosition;
    private float _maxX, _minX;
    private float _maxZ, _minZ;

    private void OnEnable()
    {
       /* EventsHolder.OnComplete += TurnOffScript;
        EventsHolder.OnGameOver += TurnOffScript;*/
    }

    private void OnDisable()
    {
       /* EventsHolder.OnComplete -= TurnOffScript;
        EventsHolder.OnGameOver -= TurnOffScript;*/
    }

    private void TurnOffScript() => enabled = false;

    private void Start() => Init();

    private void Update() => Movement();

    private void Init()
    {
        _targetPosition = transform.position; 

        _cachedCamera = Camera.main;

        _maxX = _targetPosition.x + distanceX - _cachedCamera.orthographicSize * _cachedCamera.aspect;
        _minX = _targetPosition.x - distanceX + _cachedCamera.orthographicSize * _cachedCamera.aspect;
        _maxZ = _targetPosition.z + distanceZ - _cachedCamera.orthographicSize;
        _minZ = _targetPosition.z - distanceZ + _cachedCamera.orthographicSize;
    }

    private void Movement()
    {
        if (Input.GetMouseButton(0))
        {
            float inputX = Input.GetAxis("Mouse X") * moveSpeed;
            float inputZ = Input.GetAxis("Mouse Y") * moveSpeed;

            _targetPosition = new Vector3(
                Mathf.Clamp(transform.localPosition.x - inputX, _minX, _maxX),
                transform.localPosition.y,
                Mathf.Clamp(transform.localPosition.z - inputZ, _minZ, _maxZ)
            );
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, _targetPosition, Time.deltaTime * movementTime);
    }

    //private void Movement()
    //{
    //    if (Input.GetMouseButton(0))
    //    {
    //        float inputX = Input.GetAxis("Mouse X") * moveSpeed * _cachedCamera.fieldOfView * Time.deltaTime;
    //        float inputZ = Input.GetAxis("Mouse Y") * moveSpeed * _cachedCamera.fieldOfView * Time.deltaTime;

    //        transform.localPosition = new Vector3(
    //            Mathf.Clamp(transform.localPosition.x - inputX, _minX, _maxX),
    //            transform.localPosition.y,
    //            Mathf.Clamp(transform.localPosition.z - inputZ, _minZ, _maxZ));
    //    }
    //}

    //private void Rotation()
    //{
    //    if (Input.GetKey(KeyCode.E))
    //    {
    //        rotationPoint.rotation *= Quaternion.Euler(-rotationAmount * Time.deltaTime * Vector3.up);
    //    }

    //    if (Input.GetKey(KeyCode.Q))
    //    {
    //        rotationPoint.rotation *= Quaternion.Euler(rotationAmount * Time.deltaTime * Vector3.up);
    //    }
    //}
}