using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GridUnit : MonoBehaviour
{
    public enum GridStatus
    {
        Wall,
        FloorEmpty,
        FloorColored
    }
    public GridStatus gridStatus;

    [SerializeField] private MeshRenderer _meshRenderer;
    private Level _level;

    private void Start()
    {
        _level = GetComponentInParent<Level>();
        if (gridStatus == GridStatus.FloorEmpty)
            _meshRenderer.sharedMaterial = _level.floorGridMaterial;
    }

    public void Colorize()
    {
        gridStatus = GridStatus.FloorColored;
        _meshRenderer.sharedMaterial = _level.floorPaintedMaterial;
    }
}
