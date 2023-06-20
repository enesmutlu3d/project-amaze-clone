using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int _maxLevelIndex;
    public static event Action LevelComlete;
    public static event Action LevelStart;

    private LevelAnimations _levelAnimations;
    private GameObject _loadedLevel;

    private void Awake()
    {
        _levelAnimations = GetComponent<LevelAnimations>();
        SingletonCheck();
        CheckFirstLevel();
        StartAsyncLoading();
    }

    public void LevelCompleted()
    {
        int level = PlayerPrefs.GetInt("LevelProgress");
        PlayerPrefs.SetInt("LevelProgress", level + 1);
        if (level == _maxLevelIndex)
            PlayerPrefs.SetInt("LevelProgress", 1);
        _levelAnimations.PlayLevelCompleteParticle();
        LevelComlete?.Invoke();
        StartAsyncLoading();
    }

    private void StartAsyncLoading()
    {
        int level = PlayerPrefs.GetInt("LevelProgress");
        var loadRequest = Resources.LoadAsync<GameObject>("Levels/Level_" + level);
        loadRequest.completed += AsyncLoadCompleted;
    }

    private void AsyncLoadCompleted(AsyncOperation obj)
    {
        ResourceRequest resourceRequest = (ResourceRequest)obj;

        GameObject oldLevel = null;
        if (_loadedLevel != null)
            oldLevel = _loadedLevel;
        _loadedLevel = (GameObject)Instantiate(resourceRequest.asset, Vector3.right * 30f, Quaternion.identity);
        _levelAnimations.LevelLoadAnimation(_loadedLevel, oldLevel);
        
        DOVirtual.DelayedCall(0.8f, () =>
        {
            Destroy(oldLevel);
            StartNewLevel();
        });
    }

    private void StartNewLevel()
    {
        UnloadResources();
        LevelStart?.Invoke();
    }

    private void CheckFirstLevel()
    {
        if (!PlayerPrefs.HasKey("LevelProgress"))
        {
            PlayerPrefs.SetInt("LevelProgress", 1);
        }
    }

    private void UnloadResources() => Resources.UnloadUnusedAssets();

    public static LevelManager Instance { get; private set; }
    private void SingletonCheck()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }
}
