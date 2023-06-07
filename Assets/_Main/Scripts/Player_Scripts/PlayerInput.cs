using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private GridManager _gridManager;
    [SerializeField] private Transform _unitParent;

    [HideInInspector] public bool isMovable = true;
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
                Vector3 direction = CheckInputDirection(mousePosDelta);
                int moveAmount = _gridManager.CheckMove(transform.position - _unitParent.position, direction);
                if (moveAmount > 0)
                    playerMovement.MovePlayer(direction, moveAmount);
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
}
