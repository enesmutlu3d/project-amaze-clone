using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    private Level _level;

    private void Start()
    {
        _level = GetComponentInParent<Level>();
        _meshRenderer.material = _level.ballMaterial;
    }
}
