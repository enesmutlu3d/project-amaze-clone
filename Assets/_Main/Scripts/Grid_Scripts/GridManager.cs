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
    private Level _level;

    public void OnLevelStart()
    {
        _level = GetComponentInParent<Level>();
        FillUnitDictionary();
        GetGridSize();
    }

    private void FillUnitDictionary()
    {
        int emptyFloorAmount = 0;
        foreach (Transform child in _gridUnitParent)
        {
            Vector2 key = new Vector2((int)child.localPosition.x, (int)child.localPosition.y);
            _gridUnits.Add(key, child.GetComponent<GridUnit>());
            if (child.GetComponent<GridUnit>().gridStatus == GridUnit.GridStatus.FloorEmpty)
                emptyFloorAmount++;
        }
        _level.floorGridAmount = emptyFloorAmount;
    }

    private void GetGridSize()
    {
        List<Vector2> temp = new List<Vector2>(_gridUnits.Keys);
        Vector2 minPoint = temp.Aggregate((p1, p2) => new Vector2(Math.Min(p1.x, p2.x), Math.Min(p1.y, p2.y)));
        Vector2 maxPoint = temp.Aggregate((p1, p2) => new Vector2(Math.Max(p1.x, p2.x), Math.Max(p1.y, p2.y)));
        _gridSize = new Vector2(maxPoint.x - minPoint.x + 1, maxPoint.y - minPoint.y + 1);
    }

    public int CheckMove(Vector2 currentKey, Vector2 direction)
    {
        int movableGridAmount = 0;
        float loopAmount = 0;

        if (direction.x > 0)
            loopAmount = _gridSize.x - currentKey.x;
        else if (direction.x < 0)
            loopAmount = currentKey.x + 1;
        else if (direction.y > 0)
            loopAmount = _gridSize.y - currentKey.y;
        else if (direction.y < 0)
            loopAmount = currentKey.y + 1;

        for (int i = 1; i < loopAmount; i++)
        {
            Vector2 nextKey = currentKey + direction * i;
            if (_gridUnits[nextKey].gridStatus == GridUnit.GridStatus.Wall)
                break;
            movableGridAmount++;
        }

        return movableGridAmount;
    }

    public void PaintGridUnit(Vector2 key)
    {
        if (_gridUnits[key].gridStatus != GridUnit.GridStatus.FloorColored)
        {
            _level.paintedGridAmount++;
            _level.CheckLevelProgress();
            _gridUnits[key].Colorize();
        }
    }

    private void OnEnable()
    {
        LevelManager.LevelStart += OnLevelStart;
    }

    private void OnDisable()
    {
        LevelManager.LevelStart -= OnLevelStart;
    }
}
