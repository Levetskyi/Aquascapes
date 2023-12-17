using UnityEngine;

public class SwipeDetedction : MonoBehaviour
{
    [Header("InputReader")]
    [SerializeField] private InputReader inputReader;

    [Header("Parameters")]
    [SerializeField] private float minDistance = 0.2f;
    [SerializeField] private float maxTime = 1.0f;
    [Range(0f, 1f)]
    [SerializeField] private float directionThreshhold = 0.9f;

    [SerializeField] private GameObject cameraRotator;
    [SerializeField] private float rotationSpeed = 5f;

    private Vector2 _startPosition;
    private float _startTime;

    private Vector2 _endPosition;
    private float _endTime;

    private bool _isSwiping = false;
    private Quaternion _targetRotation;
    private float rotationAmount;
    private float _maxRotaionTime = 0.25f;

    private void Start()
    {
        _targetRotation = cameraRotator.transform.rotation;
    }

    private void OnEnable()
    {
        inputReader.OnStartTouch += StartSwipe;
        inputReader.OnEndTouch += EndSwipe;
    }

    private void OnDisable()
    {
        inputReader.OnStartTouch -= StartSwipe;
        inputReader.OnEndTouch -= EndSwipe;
    }

    private void StartSwipe(Vector2 position, float time)
    {
        _startPosition = position;
        _startTime = time;

        _isSwiping = true;
    }

    private void EndSwipe(Vector2 position, float time)
    {
        _endPosition = position;
        _endTime = time;

        _isSwiping = false;

        DetectSwipe();
    }

   /* private void Update()
    {
        if (_isSwiping)
        {
            Vector2 currentSwipe = inputReader.PerformTouchPimary() - _startPosition;
            rotationAmount = currentSwipe.x * rotationSpeed * Time.deltaTime;

            _targetRotation *= Quaternion.Euler(rotationAmount * Vector3.up);
        }

        cameraRotator.transform.rotation = Quaternion.Lerp(cameraRotator.transform.rotation, _targetRotation, 0.1f);
    }*/

    private void DetectSwipe()
    {
        if (Vector3.Distance(_startPosition, _endPosition) >= minDistance 
            && (_endTime - _startTime) <= maxTime)
        {
            Vector3 directions = _endPosition - _startPosition; 
            Vector2 direction2D = (Vector2)directions.normalized;

            SwipeDirection(direction2D);
        }
    }

    private void SwipeDirection(Vector2 direction)
    {
        if (Vector2.Dot(Vector2.right, direction) > directionThreshhold) 
        {
            print("Swipe Left");

            EventsHolder.SwipeLeft();
            
        }
        else if (Vector2.Dot(Vector2.left, direction) > directionThreshhold)
        {
            print("Swipe Right");
            EventsHolder.SwipeRight();
        }
    }
}