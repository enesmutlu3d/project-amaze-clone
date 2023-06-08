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

    public void OnLevelStart()
    {
        _progressBarFill.fillAmount = 0;
        int level = PlayerPrefs.GetInt("LevelProgress");
        _currentLevelText.text = level.ToString();
        _nextLevelText.text = (level + 1).ToString();
    }

    public void OnLevelComplete()
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
