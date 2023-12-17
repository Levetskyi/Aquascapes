using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [Header("Zoom")]
    [SerializeField] private float min = 10f;
    [SerializeField] private float max = 18f;
    [SerializeField] private float zoomSensitivity = 800f;
    [SerializeField] private float zoomTime = 5f;
    //[SerializeField] private Camera uiCamera;

    private Camera _cachedCamera;
    private float _zoom;

    private void OnEnable()
    {
        /*EventsHolder.OnComplete += DisableScript;
        EventsHolder.OnGameOver += DisableScript;*/
    }

    private void OnDisable()
    {
        /*EventsHolder.OnComplete -= DisableScript;
        EventsHolder.OnGameOver -= DisableScript;*/
    }

    private void DisableScript() => enabled = false;

    private void Start() => Init();

    private void Update() => Zoom();

    private void Init()
    {
        _cachedCamera = Camera.main;
        _zoom = _cachedCamera.fieldOfView;
    }

    private void Zoom()
    {
        if (Input.mouseScrollDelta.y > 0f || Input.mouseScrollDelta.y < 0f)
        {
            float scrollValue = Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
            _zoom = Mathf.Clamp(_cachedCamera.fieldOfView - scrollValue, min, max);
        }

        _cachedCamera.fieldOfView = Mathf.Lerp(_cachedCamera.fieldOfView, _zoom, Time.deltaTime * zoomTime);
        //uiCamera.fieldOfView = _cachedCamera.fieldOfView;
    }
}