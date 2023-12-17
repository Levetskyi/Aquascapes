using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineFill : MonoBehaviour
{
    [SerializeField] private float fillDuration = 2.0f;

    private LineRenderer _lineRenderer;
    private Vector3[] linePoints;
    private int _pointsCount;

    private Coroutine _coroutine;

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Fill(List<Transform> transforms)
    {
        linePoints = new Vector3[transforms.Count];

        for (int i = 0; i < transforms.Count; i++)
        {
            linePoints[i] = transforms[i].position;
        }

        _lineRenderer.positionCount = linePoints.Length;

        _lineRenderer.SetPositions(linePoints);

        _pointsCount = transforms.Count;

        _coroutine = StartCoroutine(FillLine());
    }

    private IEnumerator FillLine()
    {
        float segmentAnimationDuration = fillDuration / _pointsCount;
        for (int i = 0; i < _pointsCount - 1; i++)
        {
            float startTime = Time.time;

            Vector3 startPosition = linePoints[i];
            Vector3 endPosition = linePoints[i + 1];

            Vector3 pos = startPosition;
            while (pos != endPosition)
            {
                float t = (Time.time - startTime) / segmentAnimationDuration;

                pos = Vector3.Lerp(startPosition, endPosition, t);

                for (int j = i + 1; j < _pointsCount; j++)
                {
                    _lineRenderer.SetPosition(j, pos);
                }

                yield return null;
            }
        }
    }
}