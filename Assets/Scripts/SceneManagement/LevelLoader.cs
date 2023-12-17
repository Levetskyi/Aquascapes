using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    private int _currentLevelIndex;

    public void TryLoadNextLevel()
    {
        int nextLevelIndex = GetLevelIndex() + 1;
        string nextLevelName = "Level_" + nextLevelIndex;

        if(SceneManager.GetSceneByName(nextLevelName).IsValid())
        {
            SceneManager.LoadScene(nextLevelName);
        }
    }

    private int GetLevelIndex()
    {
        string levelName = SceneManager.GetActiveScene().name;
        var levelNumber = Regex.Match(levelName, @"\d+\.*\d*").Value;
        _currentLevelIndex = int.Parse(levelNumber);
        return _currentLevelIndex;
    }

}