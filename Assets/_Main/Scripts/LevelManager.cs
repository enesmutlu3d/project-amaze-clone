using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private float _camTransitionFarSize, _camDefaultSize;
    [SerializeField] private int _lastLevelIndex;
    public static event Action LevelComlete;
    public static event Action LevelStart;

    private GameObject _loadedLevel;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        SingletonCheck();
        CheckFirstLevel();
        StartAsyncLoading();
    }

    public void LevelCompleted()
    {
        int level = PlayerPrefs.GetInt("LevelProgress");
        PlayerPrefs.SetInt("LevelProgress", level + 1);
        if (level == _lastLevelIndex)
            PlayerPrefs.SetInt("LevelProgress", 1);
        StartAsyncLoading();
        LevelComlete?.Invoke();
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
        _loadedLevel = (GameObject)Instantiate(resourceRequest.asset);
        LevelLoadAnimation(_loadedLevel, oldLevel);
    }

    private void LevelLoadAnimation(GameObject newLevel, GameObject oldLevel)
    {
        if (oldLevel != null)
        {
            oldLevel.transform.DOMove(Vector2.right * -15f, 0.75f).OnComplete(() => { Destroy(oldLevel); UnloadResources(); });
            _camera.DOOrthoSize(_camTransitionFarSize, 0.35f)
                .OnComplete(() => _camera.DOOrthoSize(_camDefaultSize, 0.35f));
        }

        newLevel.transform.position = Vector2.right * 15f;
        newLevel.transform.DOMove(Vector2.zero, 0.8f).OnComplete(() => LevelStart?.Invoke());
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
