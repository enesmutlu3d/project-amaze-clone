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
        }

        else if (Input.GetMouseButton(0))
        {
            Vector3 mousePosDelta = Input.mousePosition - _mouseClickedPos;

            if (mousePosDelta.magnitude > 100)
            {
                Vector2 direction = CheckInputDirection(mousePosDelta);
                int moveAmount = _gridManager.CheckMove((Vector2)transform.position - (Vector2)_unitParent.position, direction);
                if (moveAmount > 0)
                    playerMovement.MovePlayer(direction, moveAmount);
            }
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

    private void OnLevelComplete() => isMovable = false;

    private void OnLevelStart() => isMovable = true;

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
