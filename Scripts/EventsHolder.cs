using System;

public static class EventsHolder
{
    public static event Action OnCellRotated;
    public static void CellRotated() => OnCellRotated?.Invoke();

    public static event Action OnWin;
    public static void Win() => OnWin?.Invoke();
}
