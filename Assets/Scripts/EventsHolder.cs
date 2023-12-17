using System;

public static class EventsHolder
{
    #region Input

    public static event Action OnSwipeLeft;
    public static void SwipeLeft() => OnSwipeLeft?.Invoke();

    public static event Action OnSwipeRight;
    public static void SwipeRight() => OnSwipeRight?.Invoke();

    #endregion
    public static event Action OnCellTapped;
    public static void CellTapped() => OnCellTapped?.Invoke();

    public static event Action OnWin;
    public static void Win() => OnWin?.Invoke();

    public static event Action<string> OnLoadLevel;
    public static void LoadLevel(string levelName) => OnLoadLevel?.Invoke(levelName);

    public static event Action<int> OnSetQualityLevel;
    public static void SetQualityLevel(int qualityIndex) => OnSetQualityLevel?.Invoke(qualityIndex);
}