using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Transform _gridUnitParent;

    private readonly Dictionary<Vector3, GridUnit> _gridUnits = new Dictionary<Vector3, GridUnit>();
    private Vector3 _gridSize;

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
        List<Vector3> temp = new List<Vector3>(_gridUnits.Keys);
        Vector3 minPoint = temp.Aggregate((p1, p2) => new Vector3(Math.Min(p1.x, p2.x), 0, Math.Min(p1.z, p2.z)));
        Vector3 maxPoint = temp.Aggregate((p1, p2) => new Vector3(Math.Max(p1.x, p2.x), 0, Math.Max(p1.z, p2.z)));
        _gridSize = new Vector3(maxPoint.x - minPoint.x + 1, 0, maxPoint.z - minPoint.z + 1);
    }

    public int CheckMove(Vector3 currentKey, Vector3 direction)
    {
        int movableGridAmount = 0;
        float loopAmount = 0;

        if (direction.x > 0)
            loopAmount = _gridSize.x - currentKey.x;
        else if (direction.x < 0)
            loopAmount = currentKey.x;
        else if (direction.z > 0)
            loopAmount = _gridSize.z - currentKey.z;
        else if (direction.z < 0)
            loopAmount = currentKey.z;

        for (int i = 1; i < loopAmount; i++)
        {
            Vector3 nextKey = currentKey + direction * i;
            if (_gridUnits[nextKey].gridStatus != GridUnit.GridStatus.FloorEmpty)
                break;
            movableGridAmount++;
        }

        return movableGridAmount;
    }
}
