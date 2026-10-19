using UnityEngine;
using System;

public class UIController : MonoBehaviour
{
    public event Action OnTestStartRequested;
    
    [SerializeField] private UITestStart uITestStart;

    private void OnEnable()
    {
        BindEvents();
    }

    private void OnDisable()
    {
        UnbindEvents();
    }
    
    private void BindEvents()
    {
        uITestStart.OnTestStartRequested += HandleTestStart;
    }
    
    private void UnbindEvents()
    {
        uITestStart.OnTestStartRequested -= HandleTestStart;
    }
    
    private void HandleTestStart()
    {
        OnTestStartRequested?.Invoke();
    }
}
