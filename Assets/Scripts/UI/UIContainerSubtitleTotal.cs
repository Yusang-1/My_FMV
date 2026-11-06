using UnityEngine;
using System;

public class UIContainerSubtitleTotal : MonoBehaviour
{
    public Action<SubtitleData> OnShowSubtitleRequested;
    public Action<string> OnRemoveSubtitleRequested;
    public Action<ChoiceData> OnSetChoiceRequested;
    public Action<string, string> OnShowChoiceRequested;
    public Action<string> OnRemoveChoiceRequested;
    public event Action<string> InvokeChoiceAction;

    [SerializeField] private UIContainerSubtitle uIContainerSubtitle;
    [SerializeField] private UIContainerChoice uIContainerChoice;
    [SerializeField] private UIContainerTimer uIContainerTimer;

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
        OnShowSubtitleRequested += uIContainerSubtitle.ShowSubtitle;
        OnRemoveSubtitleRequested += uIContainerSubtitle.RemoveSubtitle;

        OnShowChoiceRequested += uIContainerChoice.ShowChoice;
        OnSetChoiceRequested += uIContainerChoice.SetChoiceID;
        OnRemoveChoiceRequested += uIContainerChoice.RemoveSubtitle;

        uIContainerChoice.InvokeChoiceAction += HandleChoiceAction;
    }

    private void UnbindEvents()
    {
        OnShowSubtitleRequested -= uIContainerSubtitle.ShowSubtitle;
        OnRemoveSubtitleRequested -= uIContainerSubtitle.RemoveSubtitle;

        OnShowChoiceRequested -= uIContainerChoice.ShowChoice;
        OnSetChoiceRequested -= uIContainerChoice.SetChoiceID;
        OnRemoveChoiceRequested -= uIContainerChoice.RemoveSubtitle;

        uIContainerChoice.InvokeChoiceAction -= HandleChoiceAction;
    }

    private void HandleChoiceAction(string choiceID)
    {
        InvokeChoiceAction?.Invoke(choiceID);
    }
}
