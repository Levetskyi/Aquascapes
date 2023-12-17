using System.Collections.Generic;
using UnityEngine;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private List<GameObject> islandSets = new();

    private int _currentSetIndex = 0;

    private void Awake()
    {
        UpdateIslandSets();
    }

    public void SwtichForward()
    {
        _currentSetIndex = (_currentSetIndex + 1) % islandSets.Count;
        UpdateIslandSets();
    }

    public void SwtichBackward()
    {
        _currentSetIndex = (_currentSetIndex - 1 + islandSets.Count) % islandSets.Count;
        UpdateIslandSets();
    }

    private void UpdateIslandSets()
    {
        foreach (GameObject islandSet in islandSets)
        {
            islandSet.SetActive(false);
        }

        islandSets[_currentSetIndex].SetActive(true);
    }
}