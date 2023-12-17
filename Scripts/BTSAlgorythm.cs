using System.Collections.Generic;
using UnityEngine;

public class BTSAlgorythm : MonoBehaviour
{
    [SerializeField] private Cell startCell;
    [SerializeField] private Cell endCell;
    
    private readonly List<Cell> allCells = new();

    private void OnEnable()
    {
        EventsHolder.OnCellRotated += BTS;
    }

    private void OnDisable()
    {
        EventsHolder.OnCellRotated -= BTS;
    }

    private void Start()
    {
        GetAllCells();
        BTS();
    }

    private void GetAllCells()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            if (child.TryGetComponent(out Cell cell))
            {
                allCells.Add(cell);
            }
        }
    }

    private void BTS()
    {
        HashSet<Cell> visited = new();
        Queue<Cell> queue = new();

        queue.Enqueue(startCell);
        visited.Add(startCell);

        while (queue.Count > 0)
        {
            Cell current = queue.Dequeue();

            if (current == endCell)
            {
                print("Win");
                EventsHolder.Win();
                SetColorForVisitedCells(visited);
                return;
            }

            foreach (Cell connectedCell in current.GetConnectedCells())
            {
                if (!visited.Contains(connectedCell))
                {
                    queue.Enqueue(connectedCell);
                    visited.Add(connectedCell);
                }
            }
        }

        SetColorForVisitedCells(visited);
    }

    private void SetColorForVisitedCells(HashSet<Cell> visitedCells)
    {
        foreach (Cell cell in visitedCells)
        {
            cell.SetColor(Color.blue);
        }

        foreach (Cell cell in allCells)
        {
            if (!visitedCells.Contains(cell))
            {
                cell.SetColor(Color.gray);
            }
        }
    }
}