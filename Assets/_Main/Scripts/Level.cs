using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public Material ballMaterial, floorGridMaterial;
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
