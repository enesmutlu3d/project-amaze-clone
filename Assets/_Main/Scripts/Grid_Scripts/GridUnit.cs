using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;

public class GridUnit : MonoBehaviour
{
    public enum GridStatus
    {
        Wall,
        FloorEmpty,
        FloorColored
    }
    public GridStatus gridStatus;

    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Transform _paintEffectParent;
    private Level _level;

    private void Start()
    {
        _level = GetComponentInParent<Level>();
    }

    public void Colorize()
    {
        gridStatus = GridStatus.FloorColored;
        Material[] materials = _meshRenderer.sharedMaterials;
        materials[1] = _level.floorPaintedMaterial;
        _meshRenderer.sharedMaterials = materials;
        PlayParticle();
    }

    private void PlayParticle()
    {
        ParticleSystem paintEffect = PoolManager.Instance.SpawnFloorPaintParticle(_paintEffectParent);
        ParticleSystem.MainModule settings = paintEffect.main;
        settings.startColor = _level.floorPaintEffectColor;
        paintEffect.Play();
    }
}
