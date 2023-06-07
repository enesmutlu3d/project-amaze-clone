using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    void Start()
    {
        LoadLevel();
    }

    private void LoadLevel()
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
}
