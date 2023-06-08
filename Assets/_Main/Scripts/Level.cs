using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Level : MonoBehaviour
{
    public Material ballMaterial, floorPaintedMaterial;
    [HideInInspector] public int floorGridAmount;
    [HideInInspector] public int paintedGridAmount;

    private float _levelProgress;

    public void CheckLevelProgress()
    {
        _levelProgress = (float)paintedGridAmount / (float)floorGridAmount;
        CanvasManager.Instance.UpdateProgressBar(_levelProgress);

        if (paintedGridAmount == floorGridAmount)
            DOVirtual.DelayedCall(0.2f, () => LevelManager.Instance.LevelCompleted());
    }
}
