using System;
using System.Collections;
using System.Collections.Generic;
using Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boot : MonoBehaviour
{
    public Cannon Cannon;

    private int _levelIndex;
    private Level _level;

    void Start()
    {
        LoadLevel(1);
    }

    void LoadLevel(int levelIndex)
    {
        _level?.Dispose();

        Cannon.BulletsCount = levelIndex * 5;
        _levelIndex = levelIndex;
        _level = new Level(levelIndex);
    }

    
    
    private void Update()
    {
        _level.Update();
        if (_level.IsComplete)
        {
            LoadLevel(_levelIndex + 1);
        } else if (Cannon.BulletsOut)
        {
            LoadLevel(_levelIndex);
        }
    }
}
