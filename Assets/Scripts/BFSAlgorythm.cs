using System.Collections.Generic;
using UnityEngine;

public class BFSAlgorythm : MonoBehaviour
{
    [SerializeField] private Cell startCell;
    [SerializeField] private Cell endCell;

    private readonly List<Cell> allCells = new();

    private void OnEnable()
    {
        EventsHolder.OnCellTapped += BFS;
    }

    private void OnDisable()
    {
        EventsHolder.OnCellTapped -= BFS;
    }

    private void Awake()
    {
        GetAllCells();
        BFS();
    }

    private void GetAllCells()
    {
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent(out Cell cell))
            {
                allCells.Add(cell);
            }
        }
    }

    private void BFS()
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
                //return;
            }

            foreach (Cell connectedCell in current.GetConnectedCells())
            {
                if (!visited.Contains(connectedCell))
                {
                    queue.Enqueue(connectedCell);
                    visited.Add(connectedCell);
                    connectedCell.CheckConnectionOnPoints();
                }
            }

        }
        ShowWaterOnVisitedCells(visited);
    } 

    private void ShowWaterOnVisitedCells(HashSet<Cell> visitedCells)
    {
        foreach (Cell cell in allCells)
        {
            cell.ShowWater(visitedCells.Contains(cell));
        }
    }
}