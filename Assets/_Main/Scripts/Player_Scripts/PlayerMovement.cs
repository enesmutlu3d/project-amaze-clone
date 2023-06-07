using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GridManager _gridManager;
    [SerializeField] private Transform _unitParent;

    private Vector3 _mouseClickedPos = Vector3.zero;
    private bool _isMovable = true;

    private void Update()
    {
        if (!_isMovable)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            _mouseClickedPos = Input.mousePosition;
        }

        else if (Input.GetMouseButton(0))
        {
            Vector3 mousePosDelta = Input.mousePosition - _mouseClickedPos;

            if (mousePosDelta.magnitude > 100)
            {
                Vector3 direction = CheckInputDirection(mousePosDelta);
                int moveAmount = _gridManager.CheckMove(transform.position - _unitParent.position, direction);
                if (moveAmount > 0)
                    MovePlayer(direction, moveAmount);
            }
        }
    }

    private Vector3 CheckInputDirection(Vector3 mousePosDelta)
    {
        if (Mathf.Abs(mousePosDelta.x) > Mathf.Abs(mousePosDelta.y))
        {
            if (mousePosDelta.x > 0)
                return Vector3.right;
            else
                return Vector3.left;
        }
        else
        {
            if (mousePosDelta.y > 0)
                return Vector3.forward;
            else
                return Vector3.back;
        }
    }

    private void MovePlayer(Vector3 direction, int distance)
    {
        _isMovable = false;
        transform.DOMove(transform.position + direction * distance, distance * 0.1f).OnComplete(SetMovable);
    }

    private void SetMovable () => _isMovable = true;
}
