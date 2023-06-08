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

    public void CheckLevelProgress()
    {
        _levelProgress = floorGridAmount / paintedGridAmount;
        if (paintedGridAmount == floorGridAmount)
            EndLevel();
    }

    private void EndLevel()
    {
        LevelManager.Instance.LoadLevel();
    }
}
