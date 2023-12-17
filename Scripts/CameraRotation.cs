using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] private float rotationAmount = 75f;
    [SerializeField] private float rotationTime = 0.1f;
    [SerializeField] private Transform rotationPoint;

    private Quaternion _targetRotation;

    private void OnEnable()
    {
        /*EventsHolder.OnComplete += TurnOffScript;
        EventsHolder.OnGameOver += TurnOffScript;*/
    }

    private void OnDisable()
    {
       /* EventsHolder.OnComplete -= TurnOffScript;
        EventsHolder.OnGameOver -= TurnOffScript;*/
    }

    private void TurnOffScript() => enabled = false;

    private void Start() => Init();

    private void Update() => Rotation();

    private void Init()
    {
        _targetRotation = rotationPoint.rotation;
    }

    private void Rotation()
    {
        if (Input.GetKey(KeyCode.E))
            _targetRotation *= Quaternion.Euler(-rotationAmount * Time.deltaTime * Vector3.up);

        if (Input.GetKey(KeyCode.Q))
            _targetRotation *= Quaternion.Euler(rotationAmount * Time.deltaTime * Vector3.up);

        rotationPoint.rotation = Quaternion.Lerp(rotationPoint.rotation, _targetRotation, rotationTime);
    }
}