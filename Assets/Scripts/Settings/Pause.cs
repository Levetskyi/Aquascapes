using UnityEngine;

public class Pause : MonoBehaviour
{
    private bool _isPaused = false;
    public void PauseGame()
    {
        Time.timeScale = _isPaused ? 1.0f : 0.0f;
    }
}