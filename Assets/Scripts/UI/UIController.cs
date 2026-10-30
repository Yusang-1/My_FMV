using UnityEngine;
using System;

public class UIController : MonoBehaviour
{
    public event Action OnTestStartRequested;
    public Action<SubtitleData> OnShowSubtitleRequested;
    public Action<string> OnRemoveSubtitleRequested;
    
    [SerializeField] private UITestStart uITestStart;
    [SerializeField] private UISubtitleContainer uISubtitleContainer;

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
        
        OnShowSubtitleRequested += uISubtitleContainer.ShowSubtitle;
        OnRemoveSubtitleRequested += uISubtitleContainer.RemoveSubtitle;
    }
    
    private void UnbindEvents()
    {
        uITestStart.OnTestStartRequested -= HandleTestStart;
        
        OnShowSubtitleRequested -= uISubtitleContainer.ShowSubtitle;
        OnRemoveSubtitleRequested -= uISubtitleContainer.RemoveSubtitle;
    }
    
    private void HandleTestStart()
    {
        OnTestStartRequested?.Invoke();
    }
}
