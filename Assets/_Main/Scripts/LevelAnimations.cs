using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAnimations : MonoBehaviour
{
    [SerializeField] private float _camTransitionFarSize;
    [SerializeField] private ParticleSystem _levelCompleteParticle;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    public void LevelLoadAnimation(GameObject newLevel, GameObject oldLevel)
    {
        if (oldLevel != null)
        {
            oldLevel.transform.DOMove(Vector2.right * -30f, 0.75f);
            _camera.DOOrthoSize(_camTransitionFarSize, 0.35f).SetLoops(2, LoopType.Yoyo);
        }

        newLevel.transform.DOMove(Vector2.zero, 0.75f);
    }

    public void PlayLevelCompleteParticle() => _levelCompleteParticle.Play();
}
