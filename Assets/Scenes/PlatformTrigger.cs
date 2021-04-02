using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlatformTrigger : MonoBehaviour
{
    public Collider Collider;

    public event Action OnBoxesChanged;
    
    public int BoxesCount { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Box>() == null)
            return;
        
        BoxesCount++;
        OnBoxesChanged?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Box>() == null)
            return;
        
        BoxesCount--;
        OnBoxesChanged?.Invoke();
    }
}
