using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Transform _gridUnitParent;

    private readonly Dictionary<Vector2, GridUnit> _gridUnits = new Dictionary<Vector2, GridUnit>();
    private Vector2 _gridSize;

    public void Start()
    {
        FillDictionary();
        GetGridSize();
    }

    private void FillDictionary()
    {
        foreach (Transform child in _gridUnitParent)
            _gridUnits.Add(child.localPosition, child.GetComponent<GridUnit>());
    }

    private void GetGridSize()
    {
        List<Vector2> temp = new List<Vector2>(_gridUnits.Keys);
        Vector2 minPoint = temp.Aggregate((p1, p2) => new Vector2(Math.Min(p1.x, p2.x), Math.Min(p1.y, p2.y)));
        Vector2 maxPoint = temp.Aggregate((p1, p2) => new Vector2(Math.Max(p1.x, p2.x), Math.Max(p1.y, p2.y)));
        _gridSize = new Vector2(maxPoint.x - minPoint.x + 1, maxPoint.y - minPoint.y + 1);
    }
}
