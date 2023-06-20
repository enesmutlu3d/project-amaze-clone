using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GridManager _gridManager;
    [SerializeField] private Transform _unitParent;

    private PlayerInput _playerInput;
    private PlayerVisual _playerVisual;
    private Vector2 _playerLastKey;

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerVisual = GetComponent<PlayerVisual>();
    }

    public void MovePlayer(Vector3 direction, int distance)
    {
        _playerVisual.MovementAnimation(direction, distance);
        _playerInput.isMovable = false;
        _playerLastKey = Vector2Int.RoundToInt((Vector2)transform.position - (Vector2)_unitParent.position);
        _gridManager.PaintGridUnit(_playerLastKey);
        transform.DOMove(transform.position + direction * distance, distance * 0.05f).SetEase(Ease.InQuad)
            .OnUpdate(PaintGrid)
            .OnComplete(SetMovable);
    }

    private void PaintGrid()
    {
        Vector2 currentKey = Vector2Int.RoundToInt(transform.position - _unitParent.position);
        if (_playerLastKey != currentKey)
        {
            _playerLastKey = currentKey;
            _gridManager.PaintGridUnit(_playerLastKey);
        }
    }

    private void SetMovable()
    {
        _playerInput.isMovable = true;
        _playerInput.ResetSwipeThreshold();
    }

    private void OnLevelComplete()
    {
        enabled = false;
        DOTween.Kill(transform);
    }

    private void OnEnable()
    {
        LevelManager.LevelComlete += OnLevelComplete;
    }

    private void OnDisable()
    {
        LevelManager.LevelComlete -= OnLevelComplete;
    }
}
