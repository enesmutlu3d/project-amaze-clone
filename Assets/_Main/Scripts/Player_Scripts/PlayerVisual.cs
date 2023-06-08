using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private ParticleSystem _trailParticle;
    private Level _level;

    private void Start()
    {
        _level = GetComponentInParent<Level>();
        _meshRenderer.material = _level.ballMaterial;
    }

    private void OnLevelStart()
    {
        _trailParticle.Play();
    }

    private void OnLevelComplete()
    {
        _trailParticle.Stop();
    }

    private void OnEnable()
    {
        LevelManager.LevelStart += OnLevelStart;
        LevelManager.LevelComlete += OnLevelComplete;
    }

    private void OnDisable()
    {
        LevelManager.LevelStart -= OnLevelStart;
        LevelManager.LevelComlete -= OnLevelComplete;
    }
}
