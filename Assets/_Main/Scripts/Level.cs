using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int levelIndex;
    public Material ballMaterial, floorGridMaterial, floorPaintedMaterial;
    [HideInInspector] public int floorGridAmount;
    [HideInInspector] public int paintedGridAmount;

    private float _levelProgress;

    public void OnLevelStart()
    {
        CanvasManager.Instance.UpdateProgressBar(0);
    }

    public void CheckLevelProgress()
    {
        _levelProgress = (float)paintedGridAmount / (float)floorGridAmount;
        CanvasManager.Instance.UpdateProgressBar(_levelProgress);
        if (paintedGridAmount == floorGridAmount)
        {
            CanvasManager.Instance.LevelEndProgressBarAnimation();
            EndLevel();
        }
    }

    private void EndLevel()
    {
        LevelManager.Instance.NextLevel();
    }
}
