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

    public void Colorize()
    {
        _meshRenderer.material.DOColor(Color.red, 0.3f);
    }
}
