using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GridManager _gridManager;
    [SerializeField] private Transform _unitParent;

    private PlayerInput _playerInput;
    private Vector3 _playerLastKey;

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    public void MovePlayer(Vector3 direction, int distance)
    {
        _playerInput.isMovable = false;
        _playerLastKey = Vector3Int.RoundToInt(transform.position - _unitParent.position);
        _gridManager.PaintGridUnit(_playerLastKey);
        transform.DOMove(transform.position + direction * distance, distance * 0.05f)
            .OnUpdate(PaintGrid)
            .OnComplete(SetMovable);
    }

    private void PaintGrid()
    {
        Vector3 currentKey = Vector3Int.RoundToInt(transform.position - _unitParent.position);
        if (_playerLastKey != currentKey)
        {
            _playerLastKey = currentKey;
            _gridManager.PaintGridUnit(_playerLastKey);
        }
    }

    private void SetMovable () => _playerInput.isMovable = true;
}
