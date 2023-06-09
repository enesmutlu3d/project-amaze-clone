using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private Transform _poolParent;
    [SerializeField] private ParticleSystem _floorPaintEffect;

    private readonly List<ParticleSystem> _floorPaintEffects = new List<ParticleSystem>();

    private void Awake()
    {
        SingletonCheck();
    }

    public ParticleSystem SpawnFloorPaintParticle(Transform parent)
    {
        if (_floorPaintEffects.Count == 0)
        {
            _floorPaintEffects.Add(Instantiate(_floorPaintEffect, parent));
        }
        ParticleSystem paintEffect = _floorPaintEffects[0];
        paintEffect.gameObject.SetActive(true);
        _floorPaintEffects.Remove(paintEffect);
        paintEffect.transform.parent = parent;
        paintEffect.transform.localPosition = Vector3.zero;
        DOVirtual.DelayedCall(0.5f, () => PoolFloorPaintParticle(paintEffect));

        return paintEffect;
    }

    private void PoolFloorPaintParticle(ParticleSystem paintEffect)
    {
        paintEffect.gameObject.SetActive(false);
        paintEffect.transform.parent = _poolParent;
        _floorPaintEffects.Add(paintEffect);
    }

    public static PoolManager Instance { get; private set; }
    private void SingletonCheck()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }
}
