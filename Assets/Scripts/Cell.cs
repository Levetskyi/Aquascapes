using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private GameObject water;
    [SerializeField] private List<ColliderChild> connections = new();

    private readonly List<Cell> _connectedCells = new();

    public void ShowWater(bool value)
    {
        water.SetActive(value);
    }


    public void OnChildTriggerEnter(Collider other)
    {
        if (other.transform.parent.TryGetComponent(out Cell cell))
        {
            if (!_connectedCells.Contains(cell))
            {
                _connectedCells.Add(cell);
                cell.ConnectTo(this);
            }
        }
    }

    public void OnChildTriggerExit(Collider other)
    {
        if (other.transform.parent.TryGetComponent(out Cell cell))
        {
            if (_connectedCells.Contains(cell))
            {
                _connectedCells.Remove(cell);
                cell.DisconnectFrom(this);
            }
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

    public void CheckConnectionOnPoints()
    {
        print("CheckConnectionOnPoints");
        foreach (var connection in connections)
        {
            connection.CheckForConnection();
        }
    }
}