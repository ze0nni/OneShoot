using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Scenes
{
    public class Level: IDisposable
    {
        private readonly string _sceneName;
        private readonly List<PlatformTrigger> _triggers = new List<PlatformTrigger>();

        private bool _isLevelClear;
        private float _timeoutForComplete;

        public bool IsComplete => _isLevelClear && _timeoutForComplete <= 0 && _triggers.Count > 0;
        
        public Level(int levelIndex)
        {
            _sceneName = $"level-{levelIndex}";
            SceneManager.LoadScene(_sceneName, LoadSceneMode.Additive);
        }

        public void Dispose()
        {
            foreach (var trigger in _triggers)
            {
                trigger.OnBoxesChanged -= TriggerOnOnBoxesChanged;
                trigger.gameObject.SetActive(false);
            }
            _triggers.Clear();
            
            SceneManager.UnloadScene(_sceneName);
        }

        public void Update()
        {
            if (_triggers.Count == 0)
            {
                foreach (var trigger in GameObject.FindObjectsOfType<PlatformTrigger>())
                {
                    trigger.OnBoxesChanged += TriggerOnOnBoxesChanged;
                    _triggers.Add(trigger);
                }
                return;
            }

            _timeoutForComplete -= Time.deltaTime;
        }

        private void TriggerOnOnBoxesChanged()
        {
            foreach (var trigger in _triggers)
            {
                if (trigger.BoxesCount > 0)
                {
                    _isLevelClear = false;
                    return;
                } 
            }

            _isLevelClear = true;
            _timeoutForComplete = 3;
        }
    }
}