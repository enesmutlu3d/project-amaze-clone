using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        //Test
        _meshRenderer.material.color = Color.red;
    }
}
