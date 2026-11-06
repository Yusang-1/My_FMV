using UnityEngine;
using System;

public class UIController : MonoBehaviour
{
    public event Action OnTestStartRequested;
    public Action<SubtitleData> OnShowSubtitleRequested;
    public Action<string> OnRemoveSubtitleRequested;
    public Action<ChoiceData> OnShowChoiceRequested;
    public Action<string> OnRemoveChoiceRequested;
    public event Action<string> InvokeChoiceAction;

    [SerializeField] private UITestStart uITestStart;
    [SerializeField] private UIContainerSubtitleTotal uIContainerSubtitleTotal;

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
        
        uIContainerSubtitleTotal.InvokeChoiceAction += HandleChoiceAction;

        OnShowSubtitleRequested += HandleShowSubtitle;
        OnRemoveSubtitleRequested += HandleRemoveSubtitle;
        
        OnShowChoiceRequested += HandleShowChoice;
        OnRemoveChoiceRequested += HandleRemoveChoice;
    }

    private void UnbindEvents()
    {
        uITestStart.OnTestStartRequested -= HandleTestStart;
        
        uIContainerSubtitleTotal.InvokeChoiceAction -= HandleChoiceAction;

        OnShowSubtitleRequested -= HandleShowSubtitle;
        OnRemoveSubtitleRequested -= HandleRemoveSubtitle;
        
        OnShowChoiceRequested -= HandleShowChoice;
        OnRemoveChoiceRequested -= HandleRemoveChoice;
    }

    private void HandleTestStart()
    {
        OnTestStartRequested?.Invoke();
    }
    private void HandleShowSubtitle(SubtitleData data)
    {
        uIContainerSubtitleTotal.OnShowSubtitleRequested?.Invoke(data);
    }
    private void HandleRemoveSubtitle(string id)
    {
        uIContainerSubtitleTotal.OnRemoveSubtitleRequested?.Invoke(id);
    }
    private void HandleShowChoice(ChoiceData data)
    {
        uIContainerSubtitleTotal.OnShowChoiceRequested?.Invoke(data);
    }
    private void HandleRemoveChoice(string id)
    {
        uIContainerSubtitleTotal.OnRemoveChoiceRequested?.Invoke(id);
    }
    private void HandleChoiceAction(string choiceID)
    {
        InvokeChoiceAction?.Invoke(choiceID);
    }
}
