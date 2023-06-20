using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Level : MonoBehaviour
{
    public Material ballMaterial, floorPaintedMaterial;
    public Color floorPaintEffectColor;
    [HideInInspector] public int floorGridAmount;

    private int _paintedGridAmount;
    private float _levelProgress;

    public void UpdateLevelProgress()
    {
        _paintedGridAmount++;
        _levelProgress = _paintedGridAmount / floorGridAmount;
        CanvasManager.Instance.UpdateProgressBar(_levelProgress);

        if (_paintedGridAmount == floorGridAmount)
            DOVirtual.DelayedCall(0.2f, () => LevelManager.Instance.LevelCompleted());
    }
}
