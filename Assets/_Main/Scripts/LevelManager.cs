using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private void Awake()
    {
        SingletonCheck();
    }

    public void LoadLevel()
    {
        var loadRequest = Resources.LoadAsync<GameObject>("Levels/Level_1");
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

    public static LevelManager Instance { get; private set; }
    private void SingletonCheck()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }
}
