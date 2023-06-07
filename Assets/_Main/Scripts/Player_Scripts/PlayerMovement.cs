using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GridManager _gridManager;
    [SerializeField] private Transform _unitParent;

    private void Update()
    {
        //Input Test
        if (Input.GetKeyDown(KeyCode.D))
        {
            int moveAmount = _gridManager.CheckMove(transform.position - _unitParent.position, Vector3.right);
            if (moveAmount > 0)
                MovePlayer(Vector3.right, moveAmount);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            int moveAmount = _gridManager.CheckMove(transform.position - _unitParent.position, Vector3.forward);
            Debug.Log(moveAmount);
            if (moveAmount > 0)
                MovePlayer(Vector3.forward, moveAmount);
        }
    }

    private void MovePlayer(Vector3 direction, int distance)
    {
        transform.DOMove(transform.position + direction * distance, distance * 0.1f);
    }
}
