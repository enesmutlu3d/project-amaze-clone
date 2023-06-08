using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private void Awake()
    {
        SingletonCheck();
        CheckLevelProgess();
        LoadLevel();
    }

    public void NextLevel()
    {
        int level = PlayerPrefs.GetInt("LevelProgress");
        PlayerPrefs.SetInt("LevelProgress", level + 1);
        LoadLevel();
    }

    private void LoadLevel()
    {
        int level = PlayerPrefs.GetInt("LevelProgress");
        var loadRequest = Resources.LoadAsync<GameObject>("Levels/Level_" + level);
        loadRequest.completed += LoadRequest_completed;
    }

    private void LoadRequest_completed(AsyncOperation obj)
    {
        ResourceRequest resourceRequest = (ResourceRequest)obj;
        Instantiate(resourceRequest.asset);
    }

    private void UnloadResources()
    {
        Resources.UnloadUnusedAssets();
    }

    private void CheckLevelProgess()
    {
        if (!PlayerPrefs.HasKey("LevelProgress"))
        {
            PlayerPrefs.SetInt("LevelProgress", 1);
        }
    }

    public static LevelManager Instance { get; private set; }
    private void SingletonCheck()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }
}
