using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public void RestartScene()
    {
        EventsHolder.LoadLevel(SceneManager.GetActiveScene().name);
    }

    public void TryLoadNextLevel()
    {
        int nextLevelIndex = GetLevelIndex() + 1;
        string nextLevelName = "Level_" + nextLevelIndex;

        List<string> scenesInBuild = new();
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            int lastSlash = scenePath.LastIndexOf("/");
            scenesInBuild.Add(scenePath.Substring(lastSlash + 1, scenePath.LastIndexOf(".") - lastSlash - 1));
        }

        if (scenesInBuild.Contains(nextLevelName))
        {
            EventsHolder.LoadLevel(nextLevelName);
            print("Level - " + nextLevelName + " found!");
        }
        else
        {
            EventsHolder.LoadLevel(SceneManager.GetActiveScene().name);
            print("Level - " + nextLevelName + " not found!");
        }
    }

    private int GetLevelIndex()
    {
        string levelName = SceneManager.GetActiveScene().name;
        string levelNumber = Regex.Match(levelName, @"\d+\.*\d*").Value;
        int currentLevelIndex = int.Parse(levelNumber);
        return currentLevelIndex;
    }
}