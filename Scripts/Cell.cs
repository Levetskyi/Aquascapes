using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class Cell : MonoBehaviour
{
    [SerializeField] private float rotationTime = 0.5f;

    private readonly List<Cell> _connectedCells = new();

    private bool _isRotating = false;

    private Animation _animation;
    private Renderer _renderer;

    private void Start()
    {
        _animation = GetComponent<Animation>();
        _renderer = GetComponent<Renderer>();
        _renderer.material.color = Color.gray;
    }

    public void SetColor(Color color)
    {
        _renderer.material.color = color;
    }

    public void OnMouseDown()
    {
        if (_isRotating)
        {
            return;
        }

        StartCoroutine(Rotate());
    }

    private IEnumerator Rotate()
    {
        _isRotating = true;
        _animation.Play(); 

        float elapsedTime = 0f;

        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 90, 0);

        while (elapsedTime < rotationTime)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / rotationTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation;

        _isRotating = false;

        EventsHolder.CellRotated();
    }

    public void OnChildTriggerEnter(Collider other)
    {
        Cell otherCell = other.transform.parent.GetComponent<Cell>();
        if (otherCell != null && !_connectedCells.Contains(otherCell))
        {
            _connectedCells.Add(otherCell);
            otherCell.ConnectTo(this);
        } 
    }

    public void OnChildTriggerExit(Collider other)
    {
        Cell otherCell = other.transform.parent.GetComponent<Cell>();
        if (otherCell != null && _connectedCells.Contains(otherCell))
        {
            _connectedCells.Remove(otherCell);
            otherCell.DisconnectFrom(this);
        }
    }

    public List<Cell> GetConnectedCells()
    {
        return _connectedCells;
    }

    public void ConnectTo(Cell cell)
    {
        if (!_connectedCells.Contains(cell))
        {
            _connectedCells.Add(cell);
        }
    }

    public void DisconnectFrom(Cell cell)
    {
        if (_connectedCells.Contains(cell))
        {
            _connectedCells.Remove(cell);
        }
    }
}