using UnityEngine;
using System;

public class UITestStart : MonoBehaviour
{
    public event Action OnTestStartRequested;
    
    public void OnTestStart()
    {
        OnTestStartRequested?.Invoke();
        gameObject.SetActive(false);
    }
    
    public void SetActiveTrue() => gameObject.SetActive(true);
    public void SetActiveFalse() => gameObject.SetActive(false);
}
