using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private Image _progressBarFill;
    [SerializeField] private TextMeshProUGUI _currentLevelText, _nextLevelText;

    private void Awake()
    {
        SingletonCheck();    
    }

    public void LevelEndProgressBarAnimation()
    {
        _currentLevelText.transform.DOScale(1.5f, 0.25f).SetLoops(2, LoopType.Yoyo);
        _nextLevelText.transform.DOScale(1.5f, 0.25f).SetLoops(2, LoopType.Yoyo);
    }

    public void UpdateProgressBar(float percent) => _progressBarFill.fillAmount = percent;

    public static CanvasManager Instance { get; private set; }
    private void SingletonCheck()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }
}
