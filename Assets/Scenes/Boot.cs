using System;
using System.Collections;
using System.Collections.Generic;
using Scenes;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boot : MonoBehaviour
{
    private const int MaxLevels = 4;
    
    public Cannon Cannon;
    public LevelsPanel LevelsPanel;
    [Space]
    public TextMeshProUGUI MessageText;
    public Color PositiveMessageColor;
    public Color NegativeMessageColor;
    [Space]
    public TextMeshProUGUI BulletsCount;
    
    private int _levelIndex;
    private Level _level;

    void Awake()
    {
        Cannon.OnBulletsCountChanged += OnBulletsCountChanged;
        
        LevelsPanel.SetMaxLevels(MaxLevels);
        LoadLevel(1, false);
    }

    private void OnDestroy()
    {
        Cannon.OnBulletsCountChanged -= OnBulletsCountChanged;
    }

    void LoadLevel(int levelIndex, bool restart)
    {
        _level?.Dispose();
        _level = null;
        if (levelIndex > MaxLevels)
        {
            StartCoroutine(ShowMessage("Win", true));
            return;
        }

        Cannon.SetBullets(levelIndex * 3);
        LevelsPanel.SetCurrentLevel(levelIndex);
        
        _levelIndex = levelIndex;
        _level = new Level(levelIndex);

        StartCoroutine(ShowMessage(restart
            ? $"Restart level {levelIndex}"
            : $"Start level {levelIndex}",
            !restart));
    }

    private void Update()
    {
        if (_level == null)
        {
            return;
        }
        
        _level.Update();
        if (_level.IsComplete)
        {
            LoadLevel(_levelIndex + 1, false);
        } else if (Cannon.BulletsOut)
        {
            LoadLevel(_levelIndex, true);
        }
    }

    public IEnumerator ShowMessage(string text, bool positive)
    {
        MessageText.text = text;
        MessageText.color = positive ? PositiveMessageColor : NegativeMessageColor;
        MessageText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        MessageText.gameObject.SetActive(false);
    }
    
    
    private void OnBulletsCountChanged(int count)
    {
        BulletsCount.text = $"bullets: {count}";
    }
}
