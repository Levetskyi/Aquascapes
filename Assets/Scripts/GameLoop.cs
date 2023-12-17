using UnityEngine;

public class GameLoop : MonoBehaviour
{
    [SerializeField] private GameObject winPanel;

    private void OnEnable()
    {
        EventsHolder.OnWin += ShowWinPanel;
    }

    private void OnDisable()
    {
        EventsHolder.OnWin -= ShowWinPanel;
    }

    private void Awake()
    {
        winPanel.SetActive(false);
    }

    private void ShowWinPanel()
    {
        winPanel.SetActive(true);
    }
}