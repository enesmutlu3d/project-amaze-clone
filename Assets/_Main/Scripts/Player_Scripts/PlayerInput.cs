using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private GridManager _gridManager;
    [SerializeField] private Transform _unitParent;

    [HideInInspector] public bool isMovable = false;
    private PlayerMovement playerMovement;
    private Vector3 _mouseClickedPos;
    private bool _isClicked = false;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (!isMovable)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            _mouseClickedPos = Input.mousePosition;
            _isClicked = true;
        }

        else if (Input.GetMouseButton(0))
        {
            if (!_isClicked)
                return;

            Vector3 mousePosDelta = Input.mousePosition - _mouseClickedPos;

            if (mousePosDelta.magnitude > 100)
            {
                _isClicked = false;
                Vector2 direction = CheckInputDirection(mousePosDelta);
                int moveAmount = _gridManager.CheckMove((Vector2)transform.position - (Vector2)_unitParent.position, direction);
                if (moveAmount > 0)
                    playerMovement.MovePlayer(direction, moveAmount);
            }
        }

        else if (Input.GetMouseButtonUp(0))
        {
            _isClicked = false;
        }

    }

    private Vector2 CheckInputDirection(Vector2 mousePosDelta)
    {
        if (Mathf.Abs(mousePosDelta.x) > Mathf.Abs(mousePosDelta.y))
        {
            if (mousePosDelta.x > 0)
                return Vector2.right;
            else
                return Vector2.left;
        }
        else
        {
            if (mousePosDelta.y > 0)
                return Vector2.up;
            else
                return Vector2.down;
        }
    }

    private void OnLevelComplete()
    {
        isMovable = false;
        enabled = false;
    }

    private void OnLevelStart() => DOVirtual.DelayedCall(0.1f, () => isMovable = true);

    private void OnEnable()
    {
        LevelManager.LevelComlete += OnLevelComplete;
        LevelManager.LevelStart += OnLevelStart;
    }

    private void OnDisable()
    {
        LevelManager.LevelComlete -= OnLevelComplete;
        LevelManager.LevelStart -= OnLevelStart;
    }
}
