using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private Transform _playerMesh;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private ParticleSystem _trailParticle;
    private Level _level;

    private void Start()
    {
        _level = GetComponentInParent<Level>();
        _meshRenderer.material = _level.ballMaterial;
    }

    public void OnMovement(Vector3 direction, int distance)
    {
        if (direction.x > 0 || direction.x < 0)
        {
            _playerMesh.DOScaleX(1.2f, distance * 0.05f).SetEase(Ease.InQuad).OnComplete(() =>
                _playerMesh.DOScaleX(0.35f, 0.1f).SetEase(Ease.InQuad).OnComplete(() => 
                    _playerMesh.DOScaleX(0.7f, 0.05f).SetEase(Ease.InQuad)));
        }
        else
        {
            _playerMesh.DOScaleY(1.2f, distance * 0.05f).SetEase(Ease.InQuad).OnComplete(() =>
                _playerMesh.DOScaleY(0.35f, 0.1f).SetEase(Ease.InQuad).OnComplete(() =>
                    _playerMesh.DOScaleY(0.7f, 0.05f).SetEase(Ease.InQuad)));
        }
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
